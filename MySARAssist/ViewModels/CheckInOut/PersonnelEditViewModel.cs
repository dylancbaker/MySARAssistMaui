using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging.Abstractions;
using MySARAssist.Models;

using MySARAssist.Models.People;
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
            SaveCommand = new Command(OnSaveCommand);
            NextCommand = new Command(OnNextCommand);
            DeleteCommand = new Command(OnDeleteCommand);
            BackCommand = new Command(OnBackCommand);
            CurrentMember = new Personnel();
        }

        public List<Organization> Organizations { get; private set; }
        public List<Organization> ParentOrganizations { get => OrganizationTools.GetParentOrganizations(); }

        public Personnel CurrentMember { get; private set; } = new Personnel();
        public Command CancelCommand { get; }
        public Command SaveCommand { get; }
        public Command DeleteCommand { get; }
        public Command NextCommand { get; }
        public Command BackCommand { get; }
        public bool ShowPersonnel { get; set; } = true;
        public bool ShowQualifications { get; set; } = false;

        public Guid TeamMemberID
        {
            set
            {
                Guid ID = value;
                if (ID != Guid.Empty)
                {
                    SetTeamMember(ID);
                }
                else
                {
                    CurrentMember = new Personnel();


                }
                DisplayMember();
                OnPropertyChanged(nameof(CurrentMember));
            }
        }
        public List<Qualification> PersonQualifications { get; set; } = new List<Qualification>();

        private async void SetTeamMember(Guid ID)
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
            if (CurrentMember != null)
            {
                PersonQualifications = CurrentMember.GetPersonnelQualifications();
            }
            if (CurrentMember != null && CurrentMember.MemberOrganization != null && ParentOrganizations.Any(o => o.OrganizationID == CurrentMember.MemberOrganization.ParentOrganizationID))
            {
                SelectedParentOrg = ParentOrganizations.First(o => o.OrganizationID == CurrentMember.MemberOrganization.ParentOrganizationID);
                _selectedParentOrgID = SelectedParentOrg.OrganizationID;
            }

            OnPropertyChanged(nameof(PersonQualifications));
            OnPropertyChanged(nameof(ParentOrgIndex));
        }


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
            set { _OrgIndex = value; }
        }


        private async void OnCancelCommand()
        {
            await Shell.Current.GoToAsync("..");
        }
        private async void OnSaveCommand()
        {
            if (Organizations.Count > OrgIndex) { CurrentMember.MemberOrganization = Organizations[OrgIndex]; }
            else { CurrentMember.MemberOrganization = Organizations.First(); }

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

                CurrentMember.Group = CurrentMember.MemberOrganization.OrganizationName;
                CurrentMember.OrganizationID = CurrentMember.MemberOrganization.OrganizationID;

                CurrentMember = removeBadChrs(CurrentMember);
                foreach (Qualification q in PersonQualifications)
                {
                    CurrentMember.QualificationList[q.QualificationListIndex] = q.PersonHas;
                }

                if (await new PersonnelService().UpsertItemAsync(CurrentMember))
                {
                    if (new PersonnelService().GetCurrentPersonAsync() == null)
                    {
                        new PersonnelService().setCurrentPerson(CurrentMember.PersonID);
                        App.CurrentPerson =await new PersonnelService().GetCurrentPersonAsync();
                        OnPropertyChanged(nameof(App.CurrentPerson));
                    }
                    var toast = Toast.Make("Saved", CommunityToolkit.Maui.Core.ToastDuration.Short, 14);
                    await toast.Show(new CancellationToken());
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    var toast = Toast.Make("ERROR, personnel was not saved", CommunityToolkit.Maui.Core.ToastDuration.Short, 14);
                    await toast.Show(new CancellationToken());
                }
            }
        }


        private void OnNextCommand()
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
                //hide the individual stuff, show qualifications

                ShowPersonnel = false;
                OnPropertyChanged(nameof(ShowPersonnel));
                ShowQualifications = true;
                OnPropertyChanged(nameof(ShowQualifications));
            }
        }
        private void OnBackCommand()
        {
            ShowPersonnel = true;
            OnPropertyChanged(nameof(ShowPersonnel));
            ShowQualifications = false;
            OnPropertyChanged(nameof(ShowQualifications));
        }

        private Personnel? removeBadChrs(Personnel member)
        {
            if(member == null) { return null; }
            member.Name = member.Name.removeBadChrsForQR();
            member.Callsign = member.Callsign.removeBadChrsForQR();
            member.Phone = member.Phone.removeBadChrsForQR();
            member.SpecialSkills = member.SpecialSkills.removeBadChrsForQR();
            member.Reference = member.Reference.removeBadChrsForQR();
            member.Barcode = member.Barcode.removeBadChrsForQR();
            member.Email = member.Email.removeBadChrsForQR();
            member.Pronouns = member.Pronouns.removeBadChrsForQR();
            member.Address = member.Address.removeBadChrsForQR();
            member.NOKName = member.NOKName.removeBadChrsForQR();
            member.NOKRelation = member.NOKRelation.removeBadChrsForQR();
            member.NOKPhone = member.NOKPhone.removeBadChrsForQR();
            member.D4HStatus = member.D4HStatus.removeBadChrsForQR();
            return member;
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
