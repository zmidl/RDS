﻿#pragma checksum "..\..\..\..\Views\Monitor\MonitorView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8F6F3F747D332C7CDFA5827FBF74A0DA"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
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


namespace RDS.Views.Monitor {
    
    
    /// <summary>
    /// MonitorView
    /// </summary>
    public partial class MonitorView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\..\Views\Monitor\MonitorView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel StackPanel_Reagent;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\Views\Monitor\MonitorView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel StackPanel_Sample;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\Views\Monitor\MonitorView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel DockPanel_Report;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\..\Views\Monitor\MonitorView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TextBlock_RemainingTime;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\..\Views\Monitor\MonitorView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Button_OnAndOff;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\Views\Monitor\MonitorView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Button_Help;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\..\Views\Monitor\MonitorView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Button_End;
        
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
            this.StackPanel_Reagent = ((System.Windows.Controls.StackPanel)(target));
            
            #line 11 "..\..\..\..\Views\Monitor\MonitorView.xaml"
            this.StackPanel_Reagent.PreviewMouseUp += new System.Windows.Input.MouseButtonEventHandler(this.StackPanel_Reagent_PreviewMouseUp);
            
            #line default
            #line hidden
            return;
            case 2:
            this.StackPanel_Sample = ((System.Windows.Controls.StackPanel)(target));
            
            #line 17 "..\..\..\..\Views\Monitor\MonitorView.xaml"
            this.StackPanel_Sample.PreviewMouseUp += new System.Windows.Input.MouseButtonEventHandler(this.StackPanel_Sample_PreviewMouseUp);
            
            #line default
            #line hidden
            return;
            case 3:
            this.DockPanel_Report = ((System.Windows.Controls.DockPanel)(target));
            
            #line 41 "..\..\..\..\Views\Monitor\MonitorView.xaml"
            this.DockPanel_Report.PreviewMouseUp += new System.Windows.Input.MouseButtonEventHandler(this.DockPanel_Report_PreviewMouseUp);
            
            #line default
            #line hidden
            return;
            case 4:
            this.TextBlock_RemainingTime = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.Button_OnAndOff = ((System.Windows.Controls.Button)(target));
            
            #line 66 "..\..\..\..\Views\Monitor\MonitorView.xaml"
            this.Button_OnAndOff.Click += new System.Windows.RoutedEventHandler(this.Button_OnAndOff_Click_1);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Button_Help = ((System.Windows.Controls.Button)(target));
            
            #line 67 "..\..\..\..\Views\Monitor\MonitorView.xaml"
            this.Button_Help.Click += new System.Windows.RoutedEventHandler(this.Button_Help_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Button_End = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

