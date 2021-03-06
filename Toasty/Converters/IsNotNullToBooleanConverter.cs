using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Toasty.Converters
{
	public class IsNotNullToBooleanConverter : MarkupExtension, IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value != null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		public override object ProvideValue(IServiceProvider serviceProvider) => this;
	}
}