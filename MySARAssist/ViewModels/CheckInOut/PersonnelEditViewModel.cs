using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using MySARAssist.Models;

/* Unmerged change from project 'MySARAssist (net8.0-ios)'
Before:
using System;
After:
using MySARAssist.Models.Personnel;
using MySARAssist.Models.Personnel.Personnel;
using MySARAssist.Models.Personnel.Personnel.Personnel;
using System;
*/

/* Unmerged change from project 'MySARAssist (net8.0-android)'
Before:
using System;
After:
using MySARAssist.Models.Personnel;
using MySARAssist.Models.Personnel.Personnel;
using System;
*/

/* Unmerged change from project 'MySARAssist (net8.0-windows10.0.19041.0)'
Before:
using System;
After:
using MySARAssist.Models.Personnel;
using System;
*/
using MySARAssist.Models.Personnel;
using MySARAssist.Models.Personnel.Personnel;
using MySARAssist.Models.Personnel.Personnel.Personnel;
using MySARAssist.Models.Personnel.Personnel.Personnel.Personnel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySARAssist.ViewModels.CheckInOut
{
    public class PersonnelEditViewModel : ObservableObject
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
            Organization mostPopularOrg = App.PersonnelManager.GetMostCommonOrganization();
            if (mostPopularOrg != null) { CurrentMember.MemberOrganization = mostPopularOrg; }
            DisplayMember();

        }

        public List<Organization> Organizations { get; private set; }
        public List<Organization> ParentOrganizations { get => OrganizationTools.GetParentOrganizations(); }

        public Personnel CurrentMember { get; private set; }
        public Command CancelCommand { get; }
        public Command SaveCommand { get; }
        public Command DeleteCommand { get; }
        public Command NextCommand { get; }
        public Command BackCommand { get; }

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

        private async void SetTeamMember(Guid ID)
        {
            CurrentMember = await App.PersonnelManager.GetItemAsync(ID);
        }

        private void DisplayMember()
        {
            if (CurrentMember.MemberOrganization == null && CurrentMember.OrganizationID != Guid.Empty)
            {
                CurrentMember.MemberOrganization = OrganizationTools.GetOrganization(CurrentMember.OrganizationID);
            }
            PersonQualifications = CurrentMember.GetPersonnelQualifications();
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

            ValidationResult validateResult = validateCurrent();
            if (!validateResult.success)
            {
                DependencyService.Get<Toast>().Show("ERROR: " + validateResult.message);

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

                if (await App.PersonnelManager.UpsertItemAsync(CurrentMember))
                {
                    if (App.PersonnelManager.GetCurrentTeamMember() == null)
                    {
                        App.PersonnelManager.setCurrentTeamMember(CurrentMember.PersonID);
                        App.CurrentTeamMember = App.PersonnelManager.GetCurrentTeamMember();
                        OnPropertyChanged(nameof(App.CurrentTeamMember));
                    }

                    DependencyService.Get<Toast>().Show("Team Member Saved");
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    DependencyService.Get<Toast>().Show("ERROR: Member not saved");
                }
            }
        }

        public bool ShowPersonnel { get; set; } = true;
        public bool ShowQualifications { get; set; }

        private void OnNextCommand()
        {
            ValidationResult validateResult = validateCurrent();
            if (!validateResult.success)
            {
                DependencyService.Get<Toast>().Show("ERROR: " + validateResult.message);

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

        public List<Qualification> PersonQualifications { get; set; }

        private ValidationResult validateCurrent()
        {
            ValidationResult result = new ValidationResult();
            StringBuilder msg = new StringBuilder();
            result.success = true;
            if (string.IsNullOrEmpty(CurrentMember.Name)) { result.success = false; msg.Append("You must include your full name."); }
            else if (!CurrentMember.Name.Contains(" ")) { result.success = false; msg.Append("You must include your full name."); }

            if (string.IsNullOrEmpty(CurrentMember.Email) || !CurrentMember.Email.isValidEmail()) { result.success = false; msg.Append("You must enter a valid email address."); }

            result.message = msg.ToString();
            return result;
        }

        private async void OnDeleteCommand()
        {
            if (await App.PersonnelManager.DeleteItemAsync(CurrentMember.PersonID))
            {
                if (App.CurrentTeamMember.PersonID == CurrentMember.PersonID)
                {
                    App.CurrentTeamMember = null;
                }
                DependencyService.Get<Toast>().Show("Team Member Deleted");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                DependencyService.Get<Toast>().Show("ERROR: Member not deleted");
            }
        }
    }
}
