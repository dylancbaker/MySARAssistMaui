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
using MySARAssist.Views.CheckInOut;

namespace MySARAssist.ViewModels.CheckInOut
{
    public class PersonnelEditViewModel :  ObservableValidator
    {
        public PersonnelEditViewModel()
        {
            //Organizations = OrganizationTools.GetStaticOrganizations(Guid.Empty);
            CancelCommand = new Command(OnCancelCommand);
            NextCommand = new Command(OnNextCommand);
            DeleteCommand = new Command(OnDeleteCommand);
            BackCommand = new Command(OnCancelCommand);
            this._organizationService = new OrganizationService();
            this._personnelService = new PersonnelService();
            Task.Run(async () => ParentOrganizations = await organizationService.GetItems(Guid.Empty) as List<Organization>).Wait();
            Task.Run(async () => Organizations = await organizationService.GetItems() as List<Organization>).Wait();
            //CurrentMember = new Personnel();
        }
        private OrganizationService organizationService = new OrganizationService();
        public List<Organization> Organizations { get; private set; }
        public List<Organization> ParentOrganizations { get; set; } = new List<Organization>();
        public Personnel MemberToEdit { get; private set; }= new Personnel();
        public Command CancelCommand { get; }
        public Command DeleteCommand { get; }
        public Command NextCommand { get; }
        public Command BackCommand { get; }
        public bool ShowPersonnel { get; set; } = true;

     

        public async Task SetTeamMember(Guid ID)
        {
            MemberToEdit = await _personnelService.GetItemAsync(ID);
            if(MemberToEdit == null) { MemberToEdit = new Personnel(); }
            await DisplayMember();
        }

        private async Task DisplayMember()
        {
            if(MemberToEdit != null && ( MemberToEdit.MemberOrganization == null || MemberToEdit.OrganizationID == Guid.Empty))
            {
                Organization? mostPopularOrg = await _personnelService.GetMostFrequentOrganizationAsync();
                if (mostPopularOrg != null) { MemberToEdit.MemberOrganization = mostPopularOrg; }
            }


            if (MemberToEdit != null && MemberToEdit.MemberOrganization == null && MemberToEdit.OrganizationID != Guid.Empty)
            {
                MemberToEdit.MemberOrganization = await organizationService.GetItemAsync(MemberToEdit.OrganizationID); //OrganizationTools.GetStaticOrganization(MemberToEdit.OrganizationID);
            }
            if (MemberToEdit != null && MemberToEdit.MemberOrganization != null && ParentOrganizations.Any(o => o.OrganizationID == MemberToEdit.MemberOrganization.ParentOrganizationID))
            {
                SelectedParentOrg = ParentOrganizations.First(o => o.OrganizationID == MemberToEdit.MemberOrganization.ParentOrganizationID);
                _selectedParentOrgID = SelectedParentOrg.OrganizationID;
            }

            OnPropertyChanged(nameof(ParentOrgIndex));

            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(MemberToEdit.Pronouns));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(MemberToEdit.Address));
            OnPropertyChanged(nameof(MemberToEdit.Phone));
            OnPropertyChanged(nameof(MemberToEdit.Reference));
            OnPropertyChanged(nameof(MemberToEdit.Callsign));
            OnPropertyChanged(nameof(MemberToEdit.NOKName));
            OnPropertyChanged(nameof(MemberToEdit.NOKPhone)); 
            OnPropertyChanged(nameof(MemberToEdit.NOKRelation));

        }

        public string Name { get => MemberToEdit.Name; set => MemberToEdit.Name = value; }
        public string Pronouns { get => MemberToEdit.Pronouns??string.Empty; set => MemberToEdit.Pronouns = value; }
        public string Email { get => MemberToEdit.Email; set => MemberToEdit.Email = value; }
        public string Address { get => MemberToEdit.Address ?? string.Empty; set => MemberToEdit.Address = value; }
        public string Phone { get => MemberToEdit.Phone ?? string.Empty; set => MemberToEdit.Phone = value; }
        public string Reference { get => MemberToEdit.Reference ?? string.Empty; set => MemberToEdit.Reference = value; }
        public string Callsign { get => MemberToEdit.Callsign ?? string.Empty; set => MemberToEdit.Callsign = value; }
        public string NOKName { get => MemberToEdit.NOKName ?? string.Empty; set => MemberToEdit.NOKName = value; }
        public string NOKPhone { get => MemberToEdit.NOKPhone ?? string.Empty; set => MemberToEdit.NOKPhone = value; }
        public string NOKRelation { get => MemberToEdit.NOKRelation ?? string.Empty; set => MemberToEdit.NOKRelation = value; }




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
                //Organizations = OrganizationTools.GetStaticOrganizations(_selectedParentOrgID);
                Task.Run(async () => Organizations = await organizationService.GetItems(_selectedParentOrgID) as List<Organization>).Wait();

                if (Organizations.Any(o => o.OrganizationID == MemberToEdit.OrganizationID))
                {
                    Organization selected = Organizations.First(o => o.OrganizationID == MemberToEdit.OrganizationID);
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
        private readonly PersonnelService _personnelService;
        private readonly OrganizationService _organizationService;
        public int OrgIndex
        {
            get
            {
                return _OrgIndex;
                
            }
            set { _OrgIndex = value; 
            if(Organizations.Count > _OrgIndex && _OrgIndex >= 0)
                {
                    MemberToEdit.MemberOrganization = Organizations[OrgIndex];
                }
            }
        }
        

        private async void OnCancelCommand()
        {
            await Shell.Current.GoToAsync("..");
        }
       


        private async void OnNextCommand()
        {
            List<string> issues = MemberToEdit.GetValidationIssues();
            if (issues.Any())
            {
                string text = "There were issues saving the information: ";
                string issuetext = string.Join(',', issues.ToArray());

                text += issuetext;
                SendShortToast(text);

            }
            else
            {
                try
                {
                    await SaveMemberToEdit();
                    if(App.CurrentPerson != null && MemberToEdit.PersonID == App.CurrentPerson.PersonID) { App.CurrentPerson = MemberToEdit; }
                    var toast = Toast.Make("Saved", CommunityToolkit.Maui.Core.ToastDuration.Short, 14);
                    await toast.Show(new CancellationToken());
                    await Shell.Current.GoToAsync($"{nameof(CheckInOutView)}/{nameof(PersonnelEditView)}/{nameof(EditQualificationsPage)}/?PersonnelID={MemberToEdit.ID.ToString()}");

                }

                catch (Exception ex)
                {
                    var toast = Toast.Make("ERROR, personnel was not saved", CommunityToolkit.Maui.Core.ToastDuration.Short, 14);
                    await toast.Show(new CancellationToken());
                }

            }
        }


        private async Task SaveMemberToEdit()
        {
            if (MemberToEdit != null)
            {
                if (MemberToEdit.MemberOrganization != null)
                {
                    MemberToEdit.Group = MemberToEdit.MemberOrganization.OrganizationName;
                    MemberToEdit.OrganizationID = MemberToEdit.MemberOrganization.OrganizationID;
                }

                MemberToEdit. RemoveBadChrs();
                if (MemberToEdit == null) { throw new Exception("ERROR, personnel was not saved"); }
                if (await _personnelService.UpsertItemAsync(MemberToEdit))
                {
                    if (await _personnelService.GetCurrentPersonAsync() == null)
                    {

                        _personnelService.setCurrentPerson(MemberToEdit.PersonID);
                        App.CurrentPerson = await _personnelService.GetCurrentPersonAsync();
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
            if (await _personnelService.DeleteItemAsync(MemberToEdit.PersonID))
            {
                if  (App.CurrentPerson != null && App.CurrentPerson.PersonID == MemberToEdit.PersonID)
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
