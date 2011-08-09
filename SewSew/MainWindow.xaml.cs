using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Windows.Threading;
using System.Collections.Specialized;
using System.Xml;
using System.Net;
using System.Threading;

namespace SewSew
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private oAuthTwitter _oAuth = new oAuthTwitter();
        DispatcherTimer updateTimer = new DispatcherTimer();
        string isReply;
        About abt;
        public MainWindow()
        {
            InitializeComponent();
            InitSettings();
            updateTimer.Interval = new TimeSpan(0, 5, 0);
            updateTimer.Tick += new EventHandler(updateTimer_Tick);
            Tweet_btn.IsEnabled = false;
            isReply = "";
        }

        Config conf;
        private void InitSettings()
        {
            _oAuth.ConsumerKey = SewSewAppConfig.Default.ConsumerKey;
            _oAuth.ConsumerSecret = SewSewAppConfig.Default.ConsumerSecret;
            if (IsConfigured)
            {
                _oAuth.PIN = SewSewAppConfig.Default.PIN;
                _oAuth.OAuthToken = SewSewAppConfig.Default.AuthToken;
                _oAuth.Token = SewSewAppConfig.Default.Token;
                _oAuth.TokenSecret = SewSewAppConfig.Default.SecretToken;
            }
            else
            {
                this.Hide();
                conf = new Config(ref _oAuth);
                Nullable<bool> DialogResult = conf.ShowDialog();
                if (DialogResult.Value)
                    this.Show();
                else
                    this.Close();
            }

            if (SewSewAppConfig.Default.Cache != null)
                UpdateListViewFromCache(SewSewAppConfig.Default.Cache);

            updateTimer.Start();
            
        }

        private void Tweet_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            int Count = 140 - Tweet_txt.Text.Length;
            Count_lbl.Foreground = Count < 0 ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.Gray);
            Tweet_btn.IsEnabled = Count >= 0 && Count != 140;
            Count_lbl.Content = Count;
            if (string.IsNullOrEmpty(this.Tweet_txt.Text))
                isReply = string.Empty;

            int i = this.Tweet_txt.Text.IndexOf('@');
            label1.Content = "What's Happening?";
            if (i != -1)
            {
                int i2 = this.Tweet_txt.Text.IndexOf(' ', i + 1);
                if (i2 != -1)
                {
                    label1.Content = "Reply to " + this.Tweet_txt.Text.Substring(i, i2 - i) + ":";
                }
            }
        }

        void updateTimer_Tick(object sender, EventArgs e)
        {
            UpdateTimeLine(0);
        }

        private void UpdateTimeLine(int waitMilli)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(delegate
            {
                this.Title = "SewSew - (Updating...)";
                this.Cursor = Cursors.Wait;
                
            }));
            ThreadStart start = delegate()
            {
                try
                {
                    Thread.Sleep(waitMilli);
                    string xml = _oAuth.oAuthWebRequest(
                       oAuthTwitter.Method.GET,
                       "http://twitter.com/statuses/home_timeline.xml", "");

                    XmlTextReader reader = new XmlTextReader(new StringReader(xml));

                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.InnerXml = xml;
                    SewSewAppConfig.Default.Cache = xmldoc;
                    SewSewAppConfig.Default.Save();
                    UpdateListView(xmldoc);
                }
                catch
                {
                    this.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(delegate
                    {
                        this.Cursor = Cursors.Arrow;
                        this.Title = "SewSew - Error..";
                    }));
                }
            };
            new Thread(start).Start();
        }

        private void UpdateListViewFromCache(XmlDocument xmldoc)
        {
            XmlElement root = xmldoc.DocumentElement;
            XmlNodeList lst = root.GetElementsByTagName("status");
            TextBlock textBlktxt; Image imgTmp; BitmapImage img;
            DockPanel dockpnl_tmp; string dppath;
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(delegate
            {
                this.Timeline_lstbx.Items.Clear();
                foreach (XmlNode node in lst)
                {
                    textBlktxt = new TextBlock();
                    textBlktxt.TextWrapping = TextWrapping.Wrap;
                    textBlktxt.Text = node["text"].InnerText;
                    imgTmp = new Image();
                    dppath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SewSew_cache\" + node["user"]["id"].InnerText + ".png";
                    if (File.Exists(dppath))
                    {
                        try
                        {
                            img = new BitmapImage();
                            img.BeginInit();
                            img.UriSource = new Uri(dppath, UriKind.Relative);
                            img.CacheOption = BitmapCacheOption.OnLoad;
                            img.EndInit();
                            imgTmp.Source = img;
                            imgTmp.Stretch = Stretch.Uniform;
                            imgTmp.Width = 30;
                            imgTmp.Height = 30;
                        }
                        catch { }
                    }
                    dockpnl_tmp = new DockPanel();
                    dockpnl_tmp.Children.Add(imgTmp);
                    dockpnl_tmp.Children.Add(textBlktxt);
                    textBlktxt = new TextBlock();
                    textBlktxt.Visibility = System.Windows.Visibility.Hidden;
                    textBlktxt.Text = node["user"]["screen_name"].InnerText + ":" + node["id"].InnerText;
                    dockpnl_tmp.Children.Add(textBlktxt);
                    dockpnl_tmp.ToolTip = "Double click to reply @" + node["user"]["screen_name"].InnerText;
                    Timeline_lstbx.Items.Add(dockpnl_tmp);
                }
                this.Cursor = Cursors.Arrow;
                this.Title = "SewSew";
            }));
        }

        private void UpdateListView(XmlDocument xmldoc)
        {
            XmlElement root = xmldoc.DocumentElement;
            XmlNodeList lst = root.GetElementsByTagName("status");
            TextBlock textBlktxt; Image imgTmp; BitmapImage img;
            DockPanel dockpnl_tmp;
            string dppath;

            
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(delegate
            {
                this.Timeline_lstbx.Items.Clear();
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SewSew_cache");
                foreach (XmlNode node in lst)
                {
                    textBlktxt = new TextBlock();
                    textBlktxt.TextWrapping = TextWrapping.Wrap;
                    textBlktxt.Text = node["text"].InnerText;
                    imgTmp = new Image();
                    dppath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SewSew_cache\" + node["user"]["id"].InnerText + ".png";

                    if (!File.Exists(dppath))
                        SaveImgFromURL(node["user"]["profile_image_url"].InnerText, dppath);

                    img = new BitmapImage();
                    img.BeginInit();
                    img.UriSource = new Uri(node["user"]["profile_image_url"].InnerText);
                    img.CacheOption = BitmapCacheOption.OnLoad;
                    img.EndInit();
                    imgTmp.Source = img;

                    imgTmp.Stretch = Stretch.Uniform;
                    imgTmp.Width = 30;
                    imgTmp.Height = 30;
                    dockpnl_tmp = new DockPanel();
                    dockpnl_tmp.Children.Add(imgTmp);
                    dockpnl_tmp.Children.Add(textBlktxt);
                    textBlktxt = new TextBlock();
                    textBlktxt.Visibility = System.Windows.Visibility.Hidden;
                    textBlktxt.Text = node["user"]["screen_name"].InnerText + ":" + node["id"].InnerText;
                    dockpnl_tmp.Children.Add(textBlktxt);
                    dockpnl_tmp.ToolTip = "Double click to reply @" + node["user"]["screen_name"].InnerText;
                    Timeline_lstbx.Items.Add(dockpnl_tmp);
                }

                this.Cursor = Cursors.Arrow;
                this.Title = "SewSew";
            }));
        }

        void SaveImgFromURL(string url, string fileName)
        {
            byte[] b;
            HttpWebRequest myReq =
            (HttpWebRequest)WebRequest.Create(url);
            WebResponse myResp = myReq.GetResponse();

            Stream stream = myResp.GetResponseStream();
            using (BinaryReader br = new BinaryReader(stream))
            {
                b = br.ReadBytes(500000);
            }
            myResp.Close();
            using (BinaryWriter w = new BinaryWriter(new FileStream(fileName, FileMode.Create)))
            {
                w.Write(b);
            }
        }

        public bool IsConfigured
        {
            get
            {
                return !String.IsNullOrEmpty(SewSewAppConfig.Default.ConsumerSecret) &&
                    !String.IsNullOrEmpty(SewSewAppConfig.Default.ConsumerKey) &&
                     !String.IsNullOrEmpty(SewSewAppConfig.Default.Token) &&
                      !String.IsNullOrEmpty(SewSewAppConfig.Default.SecretToken) &&
                      !String.IsNullOrEmpty(SewSewAppConfig.Default.AuthToken) &&
                    !String.IsNullOrEmpty(SewSewAppConfig.Default.PIN);
            }
        }

        private void Tweet_btn_Click(object sender, RoutedEventArgs e)
        {
            Tweet(Tweet_txt.Text);
        }

        private void Tweet(string txt)
        {

            this.Title = "SewSew - (Tweeting...)";
            this.Cursor = Cursors.Wait;
            ThreadStart start = delegate()
        {
            try
            {
                string tweet = HttpUtility.UrlEncode(txt);

                if (string.IsNullOrEmpty(isReply))
                    _oAuth.oAuthWebRequest(
                        oAuthTwitter.Method.POST,
                        "http://twitter.com/statuses/update.xml",
                        "status=" + tweet);
                else
                    _oAuth.oAuthWebRequest(
                        oAuthTwitter.Method.POST,
                        "http://twitter.com/statuses/update.xml",
                        "status=" + tweet + "&in_reply_to_status_id=" + isReply);


                UpdateTimeLine(200);
                this.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(delegate
                {
                    Tweet_txt.Text = String.Empty;
                }));
            }
            catch
            {
                this.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(delegate
                {
                    this.Cursor = Cursors.Arrow;
                    this.Title = "SewSew - Error..";
                }));
            }
        };
            new Thread(start).Start();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Timeline_lstbx.Height = this.ActualHeight - 162;
        }
     
        private void Timeline_lstbx_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DockPanel docPnl = (DockPanel)Timeline_lstbx.SelectedItem;
                TextBlock txt = (TextBlock)docPnl.Children[2];
                string[] pairs = txt.Text.Split(':');
                Tweet_txt.Text = "@" + pairs[0] + " ";
                isReply = pairs[1];
            }
            catch { }
        }

        private void Reload_btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            updateTimer.Stop();
            UpdateTimeLine(0);
            updateTimer.Start();
        }

        private void About_btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            abt = new About();
            abt.ShowDialog();
        }
    }
}