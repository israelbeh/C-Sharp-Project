﻿#pragma checksum "..\..\Appointment.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6D19CE3EB3E47899317EB430946F081E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
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


namespace ClassFun {
    
    
    /// <summary>
    /// Appointment
    /// </summary>
    public partial class Appointment : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\Appointment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ApptNotestxt;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\Appointment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ApptCloseBtn;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\Appointment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ApptAddBtn;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\Appointment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker ApptDate;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\Appointment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid ApptDataGrid;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\Appointment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ApptLabel;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\Appointment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ApptCounselorCB;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\Appointment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ApptClientCB;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\Appointment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ApptLocationCB;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\Appointment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ApptScheduleCB;
        
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
            System.Uri resourceLocater = new System.Uri("/ClassFun;component/appointment.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Appointment.xaml"
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
            this.ApptNotestxt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.ApptCloseBtn = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\Appointment.xaml"
            this.ApptCloseBtn.Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ApptAddBtn = ((System.Windows.Controls.Button)(target));
            return;
            case 4:
            this.ApptDate = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 5:
            this.ApptDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 6:
            this.ApptLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.ApptCounselorCB = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.ApptClientCB = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 9:
            this.ApptLocationCB = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 10:
            this.ApptScheduleCB = ((System.Windows.Controls.ComboBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
