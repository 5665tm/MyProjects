﻿#pragma checksum "D:\Desktop\Spritz Rider\Spritz Rider WP8\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "4240F56224FA5D6C0FF6FF9B458D4DC6"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.33440
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Spritz_Rider_WP8 {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock lbFilename;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.Slider slSpeed;
        
        internal System.Windows.Controls.Slider slReading;
        
        internal System.Windows.Controls.TextBlock lbOut;
        
        internal System.Windows.Controls.Button btOpenFile;
        
        internal System.Windows.Controls.Button btStartPause;
        
        internal System.Windows.Controls.CheckBox chSpeedUp;
        
        internal System.Windows.Controls.TextBlock lbRemTime;
        
        internal System.Windows.Controls.TextBlock lbReadingPercent;
        
        internal System.Windows.Controls.TextBlock lbSpeed;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Spritz%20Rider%20WP8;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.lbFilename = ((System.Windows.Controls.TextBlock)(this.FindName("lbFilename")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.slSpeed = ((System.Windows.Controls.Slider)(this.FindName("slSpeed")));
            this.slReading = ((System.Windows.Controls.Slider)(this.FindName("slReading")));
            this.lbOut = ((System.Windows.Controls.TextBlock)(this.FindName("lbOut")));
            this.btOpenFile = ((System.Windows.Controls.Button)(this.FindName("btOpenFile")));
            this.btStartPause = ((System.Windows.Controls.Button)(this.FindName("btStartPause")));
            this.chSpeedUp = ((System.Windows.Controls.CheckBox)(this.FindName("chSpeedUp")));
            this.lbRemTime = ((System.Windows.Controls.TextBlock)(this.FindName("lbRemTime")));
            this.lbReadingPercent = ((System.Windows.Controls.TextBlock)(this.FindName("lbReadingPercent")));
            this.lbSpeed = ((System.Windows.Controls.TextBlock)(this.FindName("lbSpeed")));
        }
    }
}

