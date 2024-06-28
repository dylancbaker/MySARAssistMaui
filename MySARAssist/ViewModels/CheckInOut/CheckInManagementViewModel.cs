using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySARAssist.ViewModels.CheckInOut
{
    public class CheckInManagementViewModel : ObservableObject
    {
        public CheckInManagementViewModel()
        {
            SignInCommand = new Command(OnSignInCommand);
            SignOutCommand = new Command(OnSignOutCommand);
            EditTeamMembersCommand = new Command(OnEditTeamMembersCommand);
            ChangeSelectedMemberCommand = new Command(OnChangeSelectedMemberCommand);
            AddMemberCommand = new Command(OnAddMember);
            EditMemberCommand = new Command(OnEditMember);

        }

        public Command SignInCommand { get; }
        public Command SignOutCommand { get; }
        public Command EditTeamMembersCommand { get; }
        public Command ChangeSelectedMemberCommand { get; }
        public Command AddMemberCommand { get; set; }
        public Command EditMemberCommand { get; }

        public void OnAppearing()
        {
            OnPropertyChanged(nameof(CurrentMemberName));
            OnPropertyChanged(nameof(AllowSignInAndOut));
        }

        public string CurrentMemberName
        {
            get
            {
                if (App.CurrentPerson != null) { return App.CurrentPerson.NameWithGroup??"-No member selected-"; }
                else { return "-No member selected-"; }
            }
        }

        public bool AllowSignInAndOut
        {
            get
            {
                return App.CurrentPerson != null;
            }
        }
        public bool ShowCreateNewUser
        {
            get { return !AllowSignInAndOut; }
        }


        private async void OnSignInCommand()
        {
            await Shell.Current.GoToAsync(nameof(Views.CheckInOut.CheckInView));
        }

        private async void OnSignOutCommand()
        {
            await Shell.Current.GoToAsync(nameof(Views.CheckInOut.CheckOutView));
        }

        private async void OnEditTeamMembersCommand()
        {
            await Shell.Current.GoToAsync(nameof(Views.CheckInOut.PersonnelListView));
        }

        private async void OnChangeSelectedMemberCommand()
        {
            await Shell.Current.GoToAsync(nameof(Views.CheckInOut.PersonnelListView));
        }

        public async void OnAddMember()
        {
            await Shell.Current.GoToAsync($"{nameof(Views.CheckInOut.PersonnelEditView)}");
        }

        public async void OnEditMember()
        {
            if(App.CurrentPerson == null) { return; }
            await Shell.Current.GoToAsync($"{nameof(Views.CheckInOut.PersonnelEditView)}?strTeamMemberID={App.CurrentPerson.PersonID}");

        }
    }
}
