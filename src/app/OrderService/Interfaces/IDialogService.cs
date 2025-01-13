using System;
using Xamarin.Forms;

namespace OrderService.Interfaces
{
    public interface IDialogService
    {
        void ShowToast(string tittle, Color? color = null, int seconds = 3);

        void ShowErrorToast(string tittle, int seconds = 3);

        void ShowSucessToast(string tittle, int seconds = 3);

        void ShowDefaultToast(string tittle, int seconds = 3);
    }
}