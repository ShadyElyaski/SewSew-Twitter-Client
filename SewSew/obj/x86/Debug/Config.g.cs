﻿#pragma checksum "..\..\..\Config.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "074919487CA5009F7C8FDB1F241E9F9A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace SewSew {
    
    
    /// <summary>
    /// Config
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class Config : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\..\Config.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas Step1_cnv;
        
        #line default
        #line hidden
        
        
        #line 7 "..\..\..\Config.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button GetPIN_btn;
        
        #line default
        #line hidden
        
        
        #line 8 "..\..\..\Config.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textBlock1;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\Config.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas Step2_cnv;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\Config.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textBlock2;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\Config.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PIN_txt;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\Config.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label1;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\Config.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Authenticate_btn;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Config.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Error_img;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SewSew;component/config.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Config.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Step1_cnv = ((System.Windows.Controls.Canvas)(target));
            return;
            case 2:
            this.GetPIN_btn = ((System.Windows.Controls.Button)(target));
            
            #line 7 "..\..\..\Config.xaml"
            this.GetPIN_btn.Click += new System.Windows.RoutedEventHandler(this.GetPIN_btn_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.textBlock1 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.Step2_cnv = ((System.Windows.Controls.Canvas)(target));
            return;
            case 5:
            this.textBlock2 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.PIN_txt = ((System.Windows.Controls.TextBox)(target));
            
            #line 12 "..\..\..\Config.xaml"
            this.PIN_txt.AddHandler(System.Windows.DataObject.PastingEvent, new System.Windows.DataObjectPastingEventHandler(this.PIN_txt_Pasting));
            
            #line default
            #line hidden
            
            #line 12 "..\..\..\Config.xaml"
            this.PIN_txt.KeyDown += new System.Windows.Input.KeyEventHandler(this.PIN_txt_KeyDown);
            
            #line default
            #line hidden
            return;
            case 7:
            this.label1 = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.Authenticate_btn = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\..\Config.xaml"
            this.Authenticate_btn.Click += new System.Windows.RoutedEventHandler(this.Authenticate_btn_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.Error_img = ((System.Windows.Controls.Image)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

