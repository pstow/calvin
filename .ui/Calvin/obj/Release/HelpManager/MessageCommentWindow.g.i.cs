﻿#pragma checksum "..\..\..\HelpManager\MessageCommentWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "88E02A20F65D356DE35494191A77496751386680E10C99A5E0E56EBFA677819B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Calvin.Touch;
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


namespace Calvin.HelpManager {
    
    
    /// <summary>
    /// MessageCommentWindow
    /// </summary>
    public partial class MessageCommentWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 29 "..\..\..\HelpManager\MessageCommentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbkText;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\HelpManager\MessageCommentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Calvin.Touch.TextBoxKeyboard kpdComment;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\HelpManager\MessageCommentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ColumnDefinition MWCol03;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\HelpManager\MessageCommentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnYes;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\HelpManager\MessageCommentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnOKCancel;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\HelpManager\MessageCommentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnNo;
        
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
            System.Uri resourceLocater = new System.Uri("/Calvin;component/helpmanager/messagecommentwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\HelpManager\MessageCommentWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            
            #line 9 "..\..\..\HelpManager\MessageCommentWindow.xaml"
            ((Calvin.HelpManager.MessageCommentWindow)(target)).Activated += new System.EventHandler(this.Window_Activated);
            
            #line default
            #line hidden
            
            #line 9 "..\..\..\HelpManager\MessageCommentWindow.xaml"
            ((Calvin.HelpManager.MessageCommentWindow)(target)).KeyDown += new System.Windows.Input.KeyEventHandler(this.Window_KeyDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.tbkText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.kpdComment = ((Calvin.Touch.TextBoxKeyboard)(target));
            return;
            case 4:
            this.MWCol03 = ((System.Windows.Controls.ColumnDefinition)(target));
            return;
            case 5:
            this.btnYes = ((System.Windows.Controls.Button)(target));
            
            #line 47 "..\..\..\HelpManager\MessageCommentWindow.xaml"
            this.btnYes.Click += new System.Windows.RoutedEventHandler(this.btnYes_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnOKCancel = ((System.Windows.Controls.Button)(target));
            
            #line 48 "..\..\..\HelpManager\MessageCommentWindow.xaml"
            this.btnOKCancel.Click += new System.Windows.RoutedEventHandler(this.btnOKCancel_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnNo = ((System.Windows.Controls.Button)(target));
            
            #line 49 "..\..\..\HelpManager\MessageCommentWindow.xaml"
            this.btnNo.Click += new System.Windows.RoutedEventHandler(this.btnNo_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

