using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinEcreo.ViewModels;

namespace XamarinEcreo
{
    public class AppShellViewModel:BaseViewModel
    {
        private bool isAdmin;

        public bool IsAdmin
        {
            get => isAdmin;
            set => SetProperty(ref isAdmin, value);
        }

        public AppShellViewModel()
        {
            MessagingCenter.Subscribe<LoginViewModel>(this, message: "admin", (sender) =>
            {
                IsAdmin=true;
            });
            MessagingCenter.Subscribe<LoginViewModel>(this, message: "user", (sender) =>
            {
                IsAdmin=false;
            });
        }


    }
}
