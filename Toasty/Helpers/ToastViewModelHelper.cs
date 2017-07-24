using Toasty.ViewModels;

namespace Toasty.Helpers
{
    public static class ToastViewModelHelper
    {
        public static void HideToast(ToastViewModelBase ToastViewModel)
        {
            if (!App.Toasts.Contains(ToastViewModel))
            {
                return;
            }
            ToastViewModel.Hide();
        }
    }
}