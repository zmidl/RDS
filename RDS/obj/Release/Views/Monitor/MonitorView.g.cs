﻿#pragma checksum "..\..\..\..\Views\Monitor\MonitorView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "90462C283C9AA168DA4BB1A9A802ECB5"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using RDS.ViewModels.Common;
using RDS.Views.Monitor;
using RDSCL;
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
using ZMCL.MyControl;
using ZMCL.MyControl.WhiteStyle;


namespace RDS.Views.Monitor {
    
    
    /// <summary>
    /// MonitorView
    /// </summary>
    public partial class MonitorView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 52 "..\..\..\..\Views\Monitor\MonitorView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle Rectangle_Prompt;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\Views\Monitor\MonitorView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal RDSCL.RD_CupRack CupRack;
        
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
            System.Uri resourceLocater = new System.Uri("/RDS;component/views/monitor/monitorview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\Monitor\MonitorView.xaml"
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
            
            #line 48 "..\..\..\..\Views\Monitor\MonitorView.xaml"
            ((RDSCL.RD_SampleRack)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.SampleRack_MouseUp);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 49 "..\..\..\..\Views\Monitor\MonitorView.xaml"
            ((RDSCL.RD_SampleRack)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.SampleRack_MouseUp);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 50 "..\..\..\..\Views\Monitor\MonitorView.xaml"
            ((RDSCL.RD_SampleRack)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.SampleRack_MouseUp);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 51 "..\..\..\..\Views\Monitor\MonitorView.xaml"
            ((RDSCL.RD_SampleRack)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.SampleRack_MouseUp);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Rectangle_Prompt = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 6:
            this.CupRack = ((RDSCL.RD_CupRack)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

