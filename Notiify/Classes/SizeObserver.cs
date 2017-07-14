using System.Windows;

namespace Notiify.Classes
{
    public static class SizeObserver
    {
        // Using a DependencyProperty as the backing store for ObservedHeight.
        // This enables animation, styling, binding, etc.
        public static readonly DependencyProperty ObservedHeightProperty =
            DependencyProperty.RegisterAttached("ObservedHeight", typeof(double), typeof(SizeObserver),
                new UIPropertyMetadata(0.0));

        // Using a DependencyProperty as the backing store for ObservedWidth.
        // This enables animation, styling, binding, etc.
        public static readonly DependencyProperty ObservedWidthProperty =
            DependencyProperty.RegisterAttached("ObservedWidth", typeof(double), typeof(SizeObserver),
                new UIPropertyMetadata(0.0));

        public static readonly DependencyProperty ObserveProperty =
            DependencyProperty.RegisterAttached("Observe", typeof(bool), typeof(SizeObserver),
                new UIPropertyMetadata(false, OnObserveChanged));

        public static bool GetObserve(FrameworkElement frameworkElement)
        {
            return (bool)frameworkElement.GetValue(ObserveProperty);
        }

        public static double GetObservedHeight(DependencyObject dependencyObject)
        {
            return (double)dependencyObject.GetValue(ObservedHeightProperty);
        }

        public static double GetObservedWidth(DependencyObject dependencyObject)
        {
            return (double)dependencyObject.GetValue(ObservedWidthProperty);
        }

        public static void SetObserve(FrameworkElement frameworkElement, bool value)
        {
            frameworkElement.SetValue(ObserveProperty, value);
        }

        public static void SetObservedHeight(DependencyObject dependencyObject, double value)
        {
            dependencyObject.SetValue(ObservedHeightProperty, value);
        }

        public static void SetObservedWidth(DependencyObject dependencyObject, double value)
        {
            dependencyObject.SetValue(ObservedWidthProperty, value);
        }

        private static void OnObserveChanged(DependencyObject dependenceyObject, DependencyPropertyChangedEventArgs e)
        {
            var frameworkElement = dependenceyObject as FrameworkElement;
            if (frameworkElement == null)
            {
                return;
            }

            if (e.NewValue is bool == false)
            {
                return;
            }

            if ((bool)e.NewValue)
            {
                frameworkElement.SizeChanged += OnSizeChanged;
            }
            else
            {
                frameworkElement.SizeChanged -= OnSizeChanged;
            }
        }

        private static void OnSizeChanged(object sender, RoutedEventArgs e)
        {
            if (!ReferenceEquals(sender, e.OriginalSource))
            {
                return;
            }

            if (e.OriginalSource is FrameworkElement frameworkElement)
            {
                SetObservedWidth(frameworkElement, frameworkElement.ActualWidth);
                SetObservedHeight(frameworkElement, frameworkElement.ActualHeight);
            }
        }
    }
}