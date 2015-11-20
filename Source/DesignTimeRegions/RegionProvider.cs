using Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace DesignTimeRegions
{
    public class RegionProvider
    {

        public static Type GetDesignTimeViewProviderType(DependencyObject obj)
        {
            return (Type)obj.GetValue(DesignTimeViewProviderTypeProperty);
        }

        public static void SetDesignTimeViewProviderType(DependencyObject obj, Type value)
        {
            obj.SetValue(DesignTimeViewProviderTypeProperty, value);
        }

        // Using a DependencyProperty as the backing store for DesignTimeViewProviderType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DesignTimeViewProviderTypeProperty =
            DependencyProperty.RegisterAttached("DesignTimeViewProviderType", typeof(Type), typeof(RegionProvider), new PropertyMetadata(null));





        public static string GetRegionName(DependencyObject obj)
        {
            return (string)obj.GetValue(RegionNameProperty);
        }

        public static void SetRegionName(DependencyObject obj, string value)
        {
            obj.SetValue(RegionNameProperty, value);
        }

        // Using a DependencyProperty as the backing store for RegionName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RegionNameProperty =
            DependencyProperty.RegisterAttached("RegionName", typeof(string), typeof(RegionProvider), new PropertyMetadata(null, RegionName_Changed));


        private static void RegionName_Changed(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var regionName = e.NewValue.ToString();
            RegionManager.SetRegionName(sender, regionName);

            if (DesignerProperties.GetIsInDesignMode(sender) == false)
            { return; } //we're in run-time, so the rest is not necessary            
            
            object designTimeView = null;
            try
            {
                var viewProviderType = GetDesignTimeViewProviderType(sender);
                var viewProvider = (DesignTimeViewProviderBase)Activator.CreateInstance(viewProviderType);
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
            else if(sender is Selector)
            {
                var itemsSource = new object[] { designTimeView };
                (sender as Selector).ItemsSource = itemsSource;
                (sender as Selector).SelectedItem = itemsSource.First();
            }
            else if(sender is ItemsControl)
            {
                (sender as ItemsControl).ItemsSource = new object[] { designTimeView };
            }            
        }

    }
}
