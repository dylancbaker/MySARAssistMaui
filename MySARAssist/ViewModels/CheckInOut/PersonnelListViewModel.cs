using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using MySARAssist.Models.People;
using MySARAssist.Services;

namespace MySARAssist.ViewModels.CheckInOut
{
    public class PersonnelListViewModel : ObservableObject
    {
        public ObservableCollection<Personnel> Items { get; private set; } = new ObservableCollection<Personnel>();
        Personnel? SelectedItem;

        public PersonnelListViewModel()
        {
            Items = new ObservableCollection<Personnel>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            //ItemTapped = new Command<Personnel>(OnItemSelected);
            EditPersonnelCommand = new Command(async (e) =>
            {
                Guid selected_memberID = Guid.Empty;
                if (e != null) { selected_memberID = new Guid(e.ToString()); }
                await OnEditPersonnel(selected_memberID);
                
            });

            SelectPersonnelCommand = new Command(async (e) =>
            {
                Guid selected_memberID = new Guid(e.ToString());

                new PersonnelService().setCurrentPerson(selected_memberID);
                App.CurrentPerson = await new PersonnelService().GetCurrentPersonAsync();
                //DependencyService.Get<Toast>().Show("Selected Member Updated");
                try
                {
                    await ExecuteLoadItemsCommand();
                }
                catch (Exception)
                {

                }

            });

            AddMemberCommand = new Command(OnAddMember);
        }

        public async Task OnEditPersonnel(Guid PersonID)
        {
            await Shell.Current.GoToAsync($"{"/CheckInOut/" + nameof(Views.CheckInOut.PersonnelEditView)}?strPersonnelID={PersonID}");

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
                var allItems = await new PersonnelService().GetItems();

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
            await Shell.Current.GoToAsync($"{"/CheckInOut/" + nameof(Views.CheckInOut.PersonnelEditView)}");
        }
    }
}
