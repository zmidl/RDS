﻿#pragma checksum "..\..\..\..\Views\Monitor\SampleView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A3A50E9098E2B4327DD7E565EB146A39"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using RDS.ViewModels.Behaviors;
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
using System.Windows.Interactivity;
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
using ZMCL.MyControl.WhiteGold;
using ZMCL.MyControl.WhiteStyle;


namespace RDS.Views.Monitor {
    
    
    /// <summary>
    /// SampleView
    /// </summary>
    public partial class SampleView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 71 "..\..\..\..\Views\Monitor\SampleView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ZMCL.MyControl.UcGlassButton Button_On;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\..\..\Views\Monitor\SampleView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ZMCL.MyControl.UcGlassButton Button_Layout;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\..\Views\Monitor\SampleView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ZMCL.MyControl.UcGlassButton Button_Import;
        
        #line default
        #line hidden
        
        
        #line 92 "..\..\..\..\Views\Monitor\SampleView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ZMCL.MyControl.UcGlassButton Button_Exit;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\..\..\Views\Monitor\SampleView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ZMCL.MyControl.UcDataGrid ucDataGrid;
        
        #line default
        #line hidden
        
        
        #line 124 "..\..\..\..\Views\Monitor\SampleView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas Canvas_SampleViewOne;
        
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
            System.Uri resourceLocater = new System.Uri("/RDS;component/views/monitor/sampleview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\Monitor\SampleView.xaml"
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
            this.Button_On = ((ZMCL.MyControl.UcGlassButton)(target));
            
            #line 75 "..\..\..\..\Views\Monitor\SampleView.xaml"
            this.Button_On.Click += new System.Windows.RoutedEventHandler(this.Button_On_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Button_Layout = ((ZMCL.MyControl.UcGlassButton)(target));
            
            #line 82 "..\..\..\..\Views\Monitor\SampleView.xaml"
            this.Button_Layout.Click += new System.Windows.RoutedEventHandler(this.Button_Layout_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Button_Import = ((ZMCL.MyControl.UcGlassButton)(target));
            
            #line 89 "..\..\..\..\Views\Monitor\SampleView.xaml"
            this.Button_Import.Click += new System.Windows.RoutedEventHandler(this.Button_Import_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Button_Exit = ((ZMCL.MyControl.UcGlassButton)(target));
            
            #line 96 "..\..\..\..\Views\Monitor\SampleView.xaml"
            this.Button_Exit.Click += new System.Windows.RoutedEventHandler(this.Button_Exit_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ucDataGrid = ((ZMCL.MyControl.UcDataGrid)(target));
            return;
            case 6:
            this.Canvas_SampleViewOne = ((System.Windows.Controls.Canvas)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

