using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;

namespace SewSew
{
    /// <summary>
    /// Interaction logic for Config.xaml
    /// </summary>
    public partial class Config : Window
    {
        private oAuthTwitter _oAuth;
        public Config(ref oAuthTwitter oAuth)
        {
            InitializeComponent();
            this._oAuth = oAuth;
            Step2_cnv.Visibility = System.Windows.Visibility.Hidden;
            Error_img.Visibility = System.Windows.Visibility.Hidden;
        }

        private void GetPIN_btn_Click(object sender, RoutedEventArgs e)
        {
            GetPIN();
        }

        private void PIN_txt_KeyDown(object sender, KeyEventArgs e)
        {
            bool b = e.Key != Key.D0 && e.Key != Key.D1 && e.Key != Key.D2 && e.Key != Key.D3 && e.Key != Key.D4 && e.Key != Key.D5 && e.Key != Key.D6 && e.Key != Key.D7 && e.Key != Key.D8 && e.Key != Key.D9 && e.Key != Key.Return && e.Key != Key.LeftCtrl && e.Key != Key.RightCtrl;
            Error_img.Visibility = b ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
            e.Handled = b;
        }

        private void PIN_txt_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                    Error_img.Visibility = System.Windows.Visibility.Visible;
                }
            }
            else
            {
                e.CancelCommand();
                Error_img.Visibility = System.Windows.Visibility.Visible;
            }
            Error_img.Visibility = System.Windows.Visibility.Hidden;
        }

        private bool IsTextAllowed(string text)
        {
            foreach (char c in text)
                if (c < 48 || c > 57)
                    return false;
            return true;
        }

        private void Authenticate_btn_Click(object sender, RoutedEventArgs e)
        {
            Authorize();
        }

        private void Authorize()
        {
            this.Cursor = Cursors.Wait;
            this.Title = "Configure - Authorizing";
            ThreadStart start = delegate()
              {
                  this.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(delegate
                             {
                                 try
                                 {

                                     if (!string.IsNullOrEmpty(PIN_txt.Text))
                                     {
                                         _oAuth.AccessTokenGet(_oAuth.OAuthToken, PIN_txt.Text.Trim());

                                         SewSewAppConfig.Default.Token = _oAuth.Token;
                                         SewSewAppConfig.Default.AuthToken = _oAuth.OAuthToken;
                                         SewSewAppConfig.Default.SecretToken = _oAuth.TokenSecret;
                                         SewSewAppConfig.Default.PIN = _oAuth.PIN;
                                         SewSewAppConfig.Default.Save();

                                         this.DialogResult = true;
                                         this.Close();
                                     }
                                     else
                                     {
                                         Error_img.Visibility = System.Windows.Visibility.Visible;
                                     }
                                 }
                                 catch
                                 {
                                     Error_img.Visibility = System.Windows.Visibility.Visible;
                                 }
                                 finally
                                 {
                                     this.Title = "Configure";
                                     this.Cursor = Cursors.Arrow;
                                 }
                             }));
              };
            new Thread(start).Start();
        }

        private void GetPIN()
        {
            this.Cursor = Cursors.Wait;
            this.Title = "Configure - Contacting Twitter";
            this.GetPIN_btn.IsEnabled = false;
            ThreadStart start = delegate()
              {

                  try
                  {
                      string authLink = _oAuth.AuthorizationLinkGet();
                      System.Diagnostics.Process.Start(authLink);
                      this.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(delegate
             {
                 Step1_cnv.Visibility = System.Windows.Visibility.Hidden;
                 Step2_cnv.Visibility = System.Windows.Visibility.Visible;
             }));

                  }
                  catch { }
                  finally
                  {
                      this.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(delegate
              {
                  this.GetPIN_btn.IsEnabled = true;
                  this.Title = "Configure";
                  this.Cursor = Cursors.Arrow;
              }));
                  }
              };
            new Thread(start).Start();
        }
    }
}