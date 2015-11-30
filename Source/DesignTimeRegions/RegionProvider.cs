using Prism.Regions;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace DesignTimeRegions
{
    public class RegionProvider
    {      
        public static DesignTimeViewProviderBase GetViewProvider(DependencyObject obj)
        {
            return (DesignTimeViewProviderBase)obj.GetValue(ViewProviderProperty);
        }

        public static void SetViewProvider(DependencyObject obj, DesignTimeViewProviderBase value)
        {
            obj.SetValue(ViewProviderProperty, value);
        }
      
        public static readonly DependencyProperty ViewProviderProperty =
            DependencyProperty.RegisterAttached("ViewProvider", typeof(DesignTimeViewProviderBase), typeof(RegionProvider), new PropertyMetadata(null, ViewProvider_Changed));

        private static void ViewProvider_Changed(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(sender) == false)
            { return; } //we're in run-time, so the rest is not necessary  

            var viewProvider = e.NewValue as DesignTimeViewProviderBase;
            var regionName = RegionManager.GetRegionName(sender);

            object designTimeView = null;
            try
            {
                viewProvider.Initialize();
                designTimeView = viewProvider.GetViewForRegion(regionName);
            }
            catch (Exception ex)
            {
                designTimeView = new TextBlock() { Text = ex.Message };
            }

            //Prism currently supports ContentControl, Selector, and ItemsControl.
            if (sender is ContentControl)
            {
                (sender as ContentControl).Content = designTimeView;
            }
            else if (sender is Selector)
            {
                var itemsSource = new object[] { designTimeView };
                (sender as Selector).ItemsSource = itemsSource;
                (sender as Selector).SelectedItem = itemsSource.First();
            }
            else if (sender is ItemsControl)
            {
                (sender as ItemsControl).ItemsSource = new object[] { designTimeView };
            }
        }
    }
}
