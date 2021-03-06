﻿#pragma checksum "..\..\Settings_Menu.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B6F7156638E68DD2B5D93CC25D00AD36265CF927B9E5353CC5487DCBDAF89F32"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using SiC_Appbar;
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


namespace SiC_Appbar {
    
    
    /// <summary>
    /// Settings_Menu
    /// </summary>
    public partial class Settings_Menu : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\Settings_Menu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton PwSettings_Random;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\Settings_Menu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton PwSettings_Static;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\Settings_Menu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PwSettings_StaticString;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\Settings_Menu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton AppBarLocSettings_Top;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\Settings_Menu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton AppBarLocSettings_Bottom;
        
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
            System.Uri resourceLocater = new System.Uri("/SiC Appbar;component/settings_menu.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Settings_Menu.xaml"
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
            
            #line 14 "..\..\Settings_Menu.xaml"
            ((SiC_Appbar.Settings_Menu)(target)).Loaded += new System.Windows.RoutedEventHandler(this.SettingsWindow_Loaded);
            
            #line default
            #line hidden
            
            #line 15 "..\..\Settings_Menu.xaml"
            ((SiC_Appbar.Settings_Menu)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.SettingsWindow_Closed);
            
            #line default
            #line hidden
            return;
            case 2:
            this.PwSettings_Random = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 3:
            this.PwSettings_Static = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 4:
            this.PwSettings_StaticString = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.AppBarLocSettings_Top = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 6:
            this.AppBarLocSettings_Bottom = ((System.Windows.Controls.RadioButton)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

