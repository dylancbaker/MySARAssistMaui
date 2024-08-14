using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging.Abstractions;
using MySarAssistModels.Interfaces;
using MySarAssistModels;
using MySarAssistModels.People;
using MySARAssist.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MySARAssist.ViewModels.CheckInOut
{
    public class PersonnelEditViewModel :  ObservableValidator
    {
        public PersonnelEditViewModel()
        {
            Organizations = OrganizationTools.GetOrganizations(Guid.Empty);
            CancelCommand = new Command(OnCancelCommand);
            NextCommand = new Command(OnNextCommand);
            DeleteCommand = new Command(OnDeleteCommand);
            //CurrentMember = new Personnel();
        }

        public List<Organization> Organizations { get; private set; }
        public List<Organization> ParentOrganizations { get => OrganizationTools.GetParentOrganizations(); }

        public Personnel CurrentMember { get; private set; }= new Personnel();
        public Command CancelCommand { get; }
        public Command DeleteCommand { get; }
        public Command NextCommand { get; }
        public Command BackCommand { get; }
        public bool ShowPersonnel { get; set; } = true;

     

        public async Task SetTeamMember(Guid ID)
        {
            CurrentMember = await new PersonnelService().GetItemAsync(ID);
            if(CurrentMember == null) { CurrentMember = new Personnel(); }
            await DisplayMember();
        }

        private async Task DisplayMember()
        {
            if(CurrentMember != null && ( CurrentMember.MemberOrganization == null || CurrentMember.OrganizationID == Guid.Empty))
            {
                Organization? mostPopularOrg = await new PersonnelService().GetMostFrequentOrganizationAsync();
                if (mostPopularOrg != null) { CurrentMember.MemberOrganization = mostPopularOrg; }
            }


            if (CurrentMember != null && CurrentMember.MemberOrganization == null && CurrentMember.OrganizationID != Guid.Empty)
            {
                CurrentMember.MemberOrganization = OrganizationTools.GetOrganization(CurrentMember.OrganizationID);
            }
            if (CurrentMember != null && CurrentMember.MemberOrganization != null && ParentOrganizations.Any(o => o.OrganizationID == CurrentMember.MemberOrganization.ParentOrganizationID))
            {
                SelectedParentOrg = ParentOrganizations.First(o => o.OrganizationID == CurrentMember.MemberOrganization.ParentOrganizationID);
                _selectedParentOrgID = SelectedParentOrg.OrganizationID;
            }

            OnPropertyChanged(nameof(ParentOrgIndex));

            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(CurrentMember.Pronouns));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(CurrentMember.Address));
            OnPropertyChanged(nameof(CurrentMember.Phone));
            OnPropertyChanged(nameof(CurrentMember.Reference));
            OnPropertyChanged(nameof(CurrentMember.Callsign));
            OnPropertyChanged(nameof(CurrentMember.NOKName));
            OnPropertyChanged(nameof(CurrentMember.NOKPhone)); 
            OnPropertyChanged(nameof(CurrentMember.NOKRelation));

        }

        public string Name { get => CurrentMember.Name; set => CurrentMember.Name = value; }
        public string Pronouns { get => CurrentMember.Pronouns??string.Empty; set => CurrentMember.Pronouns = value; }
        public string Email { get => CurrentMember.Email; set => CurrentMember.Email = value; }
        public string Address { get => CurrentMember.Address ?? string.Empty; set => CurrentMember.Address = value; }
        public string Phone { get => CurrentMember.Phone ?? string.Empty; set => CurrentMember.Phone = value; }
        public string Reference { get => CurrentMember.Reference ?? string.Empty; set => CurrentMember.Reference = value; }
        public string Callsign { get => CurrentMember.Callsign ?? string.Empty; set => CurrentMember.Callsign = value; }
        public string NOKName { get => CurrentMember.NOKName ?? string.Empty; set => CurrentMember.NOKName = value; }
        public string NOKPhone { get => CurrentMember.NOKPhone ?? string.Empty; set => CurrentMember.NOKPhone = value; }
        public string NOKRelation { get => CurrentMember.NOKRelation ?? string.Empty; set => CurrentMember.NOKRelation = value; }




        Guid _selectedParentOrgID = Guid.Empty;
        public Organization SelectedParentOrg
        {
            get
            {
                if (ParentOrganizations.Any(o => o.OrganizationID == _selectedParentOrgID)) { return ParentOrganizations.First(o => o.OrganizationID == _selectedParentOrgID); }
                else { return ParentOrganizations.First(); }
            }
            set
            {
                _selectedParentOrgID = value.OrganizationID;
                Organizations = OrganizationTools.GetOrganizations(_selectedParentOrgID);
                if (Organizations.Any(o => o.OrganizationID == CurrentMember.OrganizationID))
                {
                    Organization selected = Organizations.First(o => o.OrganizationID == CurrentMember.OrganizationID);
                    OrgIndex = Organizations.IndexOf(selected);
                }
                if (OrgIndex >= Organizations.Count) { OrgIndex = 0; }
                OnPropertyChanged(nameof(Organizations));
                OnPropertyChanged(nameof(OrgIndex));


            }
        }
        public int ParentOrgIndex
        {
            get
            {
                if (SelectedParentOrg != null && ParentOrganizations.IndexOf(SelectedParentOrg) != -1) { return ParentOrganizations.IndexOf(SelectedParentOrg); }
                else { return 0; }
            }
            set { SelectedParentOrg = ParentOrganizations[value]; }
        }


        private int _OrgIndex = 0;
        public int OrgIndex
        {
            get
            {
                return _OrgIndex;
            }
            set { _OrgIndex = value; 
            if(Organizations.Count > _OrgIndex && _OrgIndex >= 0)
                {
                    CurrentMember.MemberOrganization = Organizations[OrgIndex];
                }
            }
        }
        

        private async void OnCancelCommand()
        {
            await Shell.Current.GoToAsync($"..");
        }
       


        private async void OnNextCommand()
        {
            List<string> issues = CurrentMember.GetValidationIssues();
            if (issues.Any())
            {
                string text = "There were issues saving the information:";
                foreach (string issue in issues)
                {
                    text += "\n" + issue;
                }
                SendShortToast(text);

            }
            else
            {
                try
                {
                   await SaveCurrentPerson();
                    var toast = Toast.Make("Saved", CommunityToolkit.Maui.Core.ToastDuration.Short, 14);
                    await toast.Show(new CancellationToken());
                    await Shell.Current.GoToAsync($"CheckInOut/EditPersonnel/Qualifications/?PersonnelID={CurrentMember.ID.ToString()}");

                }

                catch
                {
                    var toast = Toast.Make("ERROR, personnel was not saved", CommunityToolkit.Maui.Core.ToastDuration.Short, 14);
                    await toast.Show(new CancellationToken());
                }

            }
        }


        private async Task SaveCurrentPerson()
        {
            if (CurrentMember != null)
            {
                if (CurrentMember.MemberOrganization != null)
                {
                    CurrentMember.Group = CurrentMember.MemberOrganization.OrganizationName;
                    CurrentMember.OrganizationID = CurrentMember.MemberOrganization.OrganizationID;
                }

                CurrentMember. RemoveBadChrs();
                if (CurrentMember == null) { throw new Exception("ERROR, personnel was not saved"); }
                if (await new PersonnelService().UpsertItemAsync(CurrentMember))
                {
                    if (await new PersonnelService().GetCurrentPersonAsync() == null)
                    {

                        new PersonnelService().setCurrentPerson(CurrentMember.PersonID);
                        App.CurrentPerson = await new PersonnelService().GetCurrentPersonAsync();
                        OnPropertyChanged(nameof(App.CurrentPerson));
                        return;

                    }
                }
                else
                {
                    throw new Exception("ERROR, personnel was not saved");

                }
            }
        }

    

       

      

        private async void OnDeleteCommand()
        {
            if (await new PersonnelService().DeleteItemAsync(CurrentMember.PersonID))
            {
                if  (App.CurrentPerson != null && App.CurrentPerson.PersonID == CurrentMember.PersonID)
                {
                    App.CurrentPerson = null;
                }
                SendShortToast("Person has been deleted");
                
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                SendShortToast("ERROR! Personnel was not deleted");
            }
        }

        private async void SendShortToast(string text)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            ToastDuration duration = ToastDuration.Short;
            double fontSize = 14;

            var toast = Toast.Make(text, duration, fontSize);

            await toast.Show(cancellationTokenSource.Token);
        }
    }
}
