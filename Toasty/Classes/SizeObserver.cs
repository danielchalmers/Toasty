using System.Windows;

namespace Toasty.Classes
{
	public static class SizeObserver
	{
		public static readonly DependencyProperty ObservedHeightProperty =
			DependencyProperty.RegisterAttached("ObservedHeight", typeof(double), typeof(SizeObserver),
				new UIPropertyMetadata(0.0));

		public static readonly DependencyProperty ObservedWidthProperty =
			DependencyProperty.RegisterAttached("ObservedWidth", typeof(double), typeof(SizeObserver),
				new UIPropertyMetadata(0.0));

		public static readonly DependencyProperty ObserveProperty =
			DependencyProperty.RegisterAttached("Observe", typeof(bool), typeof(SizeObserver),
				new UIPropertyMetadata(false, OnObserveChanged));

		public static bool GetObserve(FrameworkElement frameworkElement) => (bool)frameworkElement.GetValue(ObserveProperty);

		public static double GetObservedHeight(DependencyObject dependencyObject) => (double)dependencyObject.GetValue(ObservedHeightProperty);

		public static double GetObservedWidth(DependencyObject dependencyObject) => (double)dependencyObject.GetValue(ObservedWidthProperty);

		public static void SetObserve(FrameworkElement frameworkElement, bool value) => frameworkElement.SetValue(ObserveProperty, value);

		public static void SetObservedHeight(DependencyObject dependencyObject, double value) => dependencyObject.SetValue(ObservedHeightProperty, value);

		public static void SetObservedWidth(DependencyObject dependencyObject, double value) => dependencyObject.SetValue(ObservedWidthProperty, value);

		private static void OnObserveChanged(DependencyObject dependenceyObject, DependencyPropertyChangedEventArgs e)
		{
			if (dependenceyObject is not FrameworkElement frameworkElement || e.NewValue is not bool newValue)
				return;

			if (newValue)
				frameworkElement.SizeChanged += OnSizeChanged;
			else
				frameworkElement.SizeChanged -= OnSizeChanged;
		}

		private static void OnSizeChanged(object sender, RoutedEventArgs e)
		{
			if (!ReferenceEquals(sender, e.OriginalSource))
				return;

			if (e.OriginalSource is FrameworkElement frameworkElement)
			{
				SetObservedWidth(frameworkElement, frameworkElement.ActualWidth);
				SetObservedHeight(frameworkElement, frameworkElement.ActualHeight);
			}
		}
	}
}