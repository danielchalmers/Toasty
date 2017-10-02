using System.Windows;

namespace Toasty.Classes
{
    public static class Alert
    {
        public static MessageBoxResult Show(
            string text,
            MessageBoxButton button = MessageBoxButton.OK,
            MessageBoxImage image = MessageBoxImage.Information,
            MessageBoxResult defaultButton = MessageBoxResult.OK)
        {
            return MessageBox.Show(
                text,
                App.Title,
                button,
                image,
                defaultButton);
        }
    }
}