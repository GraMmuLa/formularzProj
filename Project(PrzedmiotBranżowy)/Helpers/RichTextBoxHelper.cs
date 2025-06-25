using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Project_PrzedmiotBranżowy_.Helpers
{
    class RichTextBoxHelper
    {
        public static readonly DependencyProperty DocumentProperty =
            DependencyProperty.RegisterAttached("Document",
                typeof(FlowDocument),
                typeof(RichTextBoxHelper),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnDocumentChanged));

        public static FlowDocument GetDocument(DependencyObject obj)
        {
            return (FlowDocument)obj.GetValue(DocumentProperty);
        }

        public static void SetDocument(DependencyObject obj, FlowDocument value)
        {
            obj.SetValue(DocumentProperty, value);
        }

        private static void OnDocumentChanged(DependencyObject obj,
            DependencyPropertyChangedEventArgs e)
        {
            if(obj is RichTextBox rtb)
            {
                rtb.Document = e.NewValue as FlowDocument;
            }
        }
    }
}
