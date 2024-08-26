using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using MySarAssistModels.People;
using MySARAssist.Services;

namespace MySARAssist.ViewModels.CheckInOut
{
    public class PersonnelListViewModel : ObservableObject
    {
        public ObservableCollection<Personnel> Items { get; private set; } = new ObservableCollection<Personnel>();
        Personnel? SelectedItem;
        private readonly PersonnelService _personnelService;

        public PersonnelListViewModel()
        {
            this._personnelService = new PersonnelService();
            IsRefreshing = false;
            EditPersonnelCommand = new Command(async (e) =>
            {
                Guid selected_memberID = Guid.Empty;
                if (e != null) { selected_memberID = new Guid(e.ToString()); }
                await OnEditPersonnel(selected_memberID);

            });

            SelectPersonnelCommand = new Command(async (e) =>
            {
                Guid selected_memberID = new Guid(e.ToString());

                _personnelService.setCurrentPerson(selected_memberID);
                App.CurrentPerson = await _personnelService.GetCurrentPersonAsync();
                //DependencyService.Get<Toast>().Show("Selected Member Updated");
                try
                {
                    //await ExecuteLoadItemsCommand();

                    await Shell.Current.GoToAsync($"{nameof(Views.CheckInOut.CheckInOutView)}");

                }
                catch (Exception)
                {

                }

            });

            AddMemberCommand = new Command(OnAddMember);



            Items = new ObservableCollection<Personnel>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            //ItemTapped = new Command<Personnel>(OnItemSelected);
    
        }

        public async Task OnEditPersonnel(Guid PersonID)
        {
            await Shell.Current.GoToAsync($"{nameof(Views.CheckInOut.CheckInOutView)}/{nameof(Views.CheckInOut.PersonnelEditView)}?PersonnelID={PersonID}");

        }

        public async void OnAppearing()
        {

            SelectedItem = null;
            await ExecuteLoadItemsCommand();

        }


        public Command EditPersonnelCommand { get; }
        public Command SelectPersonnelCommand { get; }
        public Command LoadItemsCommand { get; }
        public bool IsRefreshing { get; set; }
        public Command AddMemberCommand { get; set; }

        async public Task ExecuteLoadItemsCommand()
        {
            IsRefreshing = true;
            OnPropertyChanged(nameof(IsRefreshing));
            try
            {

                Items.Clear();
                //new Mitigation_Assessment().deleteBlankAssessments(); //for debug reasons
                var allItems = await _personnelService.GetItems();

                foreach (Personnel m in allItems)
                {
                    Items.Add(m);
                }

            }
            catch (Exception)
            {

            }
            finally
            {
                IsRefreshing = false;
                OnPropertyChanged(nameof(IsRefreshing));
                OnPropertyChanged(nameof(Items));

            }
        }

        public async void OnItemSelected(Personnel item)
        {
            if (item == null)
                return;
          await  OnEditPersonnel(item.PersonID);
            
        }
        public async void OnAddMember()
        {
            await Shell.Current.GoToAsync($"{nameof(Views.CheckInOut.CheckInOutView)}/{nameof(Views.CheckInOut.PersonnelEditView)}");
        }
    }
}
