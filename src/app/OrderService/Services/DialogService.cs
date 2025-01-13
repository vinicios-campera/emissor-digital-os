using Acr.UserDialogs;
using OrderService.Interfaces;
using System;
using Xamarin.Forms;

namespace OrderService.Services
{
    public class DialogService : IDialogService
    {
        public void ShowDefaultToast(string tittle, int seconds = 3) => ShowToast(tittle, null, seconds);

        public void ShowErrorToast(string tittle, int seconds = 3) => ShowToast(tittle, Color.Red, seconds);

        public void ShowSucessToast(string tittle, int seconds = 3) => ShowToast(tittle, Color.Green, seconds);

        public void ShowToast(string tittle, Color? color = null, int seconds = 3)
        {
            var toast = new ToastConfig(tittle);
            toast.Duration = TimeSpan.FromSeconds(seconds);
            toast.BackgroundColor = color ?? toast.BackgroundColor;
            UserDialogs.Instance.Toast(toast);
        }
    }
}