﻿#pragma checksum "..\..\InputData.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "3B43FE3CDC3A03349B29B0323D1DF35B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34011
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


namespace NeuroAnd {
    
    
    /// <summary>
    /// InputData
    /// </summary>
    public partial class InputData : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\InputData.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btConfirm;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\InputData.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbW03;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\InputData.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbW13;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\InputData.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbW23;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\InputData.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbN;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/NeuroAnd;component/inputdata.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\InputData.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.btConfirm = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\InputData.xaml"
            this.btConfirm.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.tbW03 = ((System.Windows.Controls.TextBox)(target));
            
            #line 14 "..\..\InputData.xaml"
            this.tbW03.KeyDown += new System.Windows.Input.KeyEventHandler(this.TextBox_EnterConfirm);
            
            #line default
            #line hidden
            
            #line 14 "..\..\InputData.xaml"
            this.tbW03.GotMouseCapture += new System.Windows.Input.MouseEventHandler(this.tb_GotMouseCapture);
            
            #line default
            #line hidden
            
            #line 14 "..\..\InputData.xaml"
            this.tbW03.GotFocus += new System.Windows.RoutedEventHandler(this.tb_GotFocus);
            
            #line default
            #line hidden
            return;
            case 3:
            this.tbW13 = ((System.Windows.Controls.TextBox)(target));
            
            #line 15 "..\..\InputData.xaml"
            this.tbW13.KeyDown += new System.Windows.Input.KeyEventHandler(this.TextBox_EnterConfirm);
            
            #line default
            #line hidden
            
            #line 15 "..\..\InputData.xaml"
            this.tbW13.GotMouseCapture += new System.Windows.Input.MouseEventHandler(this.tb_GotMouseCapture);
            
            #line default
            #line hidden
            
            #line 15 "..\..\InputData.xaml"
            this.tbW13.GotFocus += new System.Windows.RoutedEventHandler(this.tb_GotFocus);
            
            #line default
            #line hidden
            return;
            case 4:
            this.tbW23 = ((System.Windows.Controls.TextBox)(target));
            
            #line 16 "..\..\InputData.xaml"
            this.tbW23.KeyDown += new System.Windows.Input.KeyEventHandler(this.TextBox_EnterConfirm);
            
            #line default
            #line hidden
            
            #line 16 "..\..\InputData.xaml"
            this.tbW23.GotMouseCapture += new System.Windows.Input.MouseEventHandler(this.tb_GotMouseCapture);
            
            #line default
            #line hidden
            
            #line 16 "..\..\InputData.xaml"
            this.tbW23.GotFocus += new System.Windows.RoutedEventHandler(this.tb_GotFocus);
            
            #line default
            #line hidden
            return;
            case 5:
            this.tbN = ((System.Windows.Controls.TextBox)(target));
            
            #line 21 "..\..\InputData.xaml"
            this.tbN.KeyDown += new System.Windows.Input.KeyEventHandler(this.TextBox_EnterConfirm);
            
            #line default
            #line hidden
            
            #line 21 "..\..\InputData.xaml"
            this.tbN.GotMouseCapture += new System.Windows.Input.MouseEventHandler(this.tb_GotMouseCapture);
            
            #line default
            #line hidden
            
            #line 21 "..\..\InputData.xaml"
            this.tbN.GotFocus += new System.Windows.RoutedEventHandler(this.tb_GotFocus);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

