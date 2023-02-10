using EcreoLibraryStandard;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;
using XamarinEcreo.Models;
using XamarinEcreo.Services;
using XamarinEcreo.Views;

namespace XamarinEcreo.ViewModels
{
    public class AbsenceReportViewModel : ViewModelBase
    {

        string reason;
        public string Reason { get => reason; set => SetProperty(ref reason, value); }
        string firstname;
        public string Firstname { get => UserGet.Firstname; }
        readonly IUserDbService _userDbService;
        public AbsenceStatus absenceReport { get; set; }
        public AsyncCommand AbsenceReportCmd { get; set; }
        public string[] AllRoles { get; } = Enum.GetNames(typeof(AbsenceStatusRole));

        private AbsenceStatusRole selectedRole;
        public AbsenceStatusRole SelectedRole
        {
            get => selectedRole;
            set
            {
                SetProperty(ref selectedRole, value);
            }
        }

        public AbsenceReportViewModel()
        {
            Title = "Meld sygdom";
            AbsenceReportCmd = new AsyncCommand(UpdateCommand);
            _userDbService = DependencyService.Get<IUserDbService>();// new UserDbService();
        }
        async Task UpdateCommand()
        {
            UserGet.CurrentAbsenceStatus = SelectedRole;
            await _userDbService.UpdateUserAbsenceAsync(UserGet);
            //await Shell.Current.GoToAsync($"..?BaseID={User.BaseID}");
            if (UserGet.CurrentAbsenceStatus == AbsenceStatusRole.OnSite || UserGet.CurrentAbsenceStatus ==  AbsenceStatusRole.Late)
            {
                await Application.Current.MainPage.Navigation.PopAsync();
                await Shell.Current.GoToAsync($"///{nameof(OfficeStatusPage)}?BaseID={UserGet.BaseID}");
                
            }
            else if (UserGet.CurrentAbsenceStatus == AbsenceStatusRole.Home)
            {
                await Application.Current.MainPage.Navigation.PopAsync();
                await Shell.Current.GoToAsync($"///{nameof(HomeStatusPage)}?BaseID={UserGet.BaseID}");
            }
            else if (UserGet.CurrentAbsenceStatus == AbsenceStatusRole.Sick)
            {
                await Application.Current.MainPage.Navigation.PopAsync();
                await Shell.Current.GoToAsync($"///{nameof(AbsenceStatusPage)}?BaseID={UserGet.BaseID}");
            }

        }

    }
}
