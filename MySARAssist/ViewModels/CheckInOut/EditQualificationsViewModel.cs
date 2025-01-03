﻿using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using MySarAssistModels.People;
using MySARAssist.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySARAssist.Views.CheckInOut;
using Microsoft.Extensions.Logging;

namespace MySARAssist.ViewModels.CheckInOut
{
    public class EditQualificationsViewModel : ObservableObject
    {
        private readonly PersonnelService _personnelService;
        private readonly ILogger<MainPage> _logger;

        public Personnel CurrentMember { get; private set; } = new Personnel();

        public Guid TeamMemberID
        {
            set
            {
                Guid ID = value;
                if (ID != Guid.Empty) { SetTeamMember(ID); }
                else { CurrentMember = new Personnel(); }
                DisplayMember();
                OnPropertyChanged(nameof(CurrentMember));
            }
        }
        public List<Qualification> PersonQualifications { get; set; } = new List<Qualification>();

        public Command BackCommand { get; }

        public Command SaveCommand { get; }





        public EditQualificationsViewModel(ILogger<MainPage> logger)
        {
            BackCommand = new Command(OnBackCommand);
            SaveCommand = new Command(OnSaveCommand);
            this._personnelService = new PersonnelService();
            this._logger = logger;
        }

        private async void OnBackCommand(object obj)
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void SetTeamMember(Guid ID)
        {
            CurrentMember = await _personnelService.GetItemAsync(ID);
            if (CurrentMember == null) { CurrentMember = new Personnel(); }
            DisplayMember();
        }

        private void DisplayMember()
        {

            if (CurrentMember != null)
            {
                PersonQualifications = CurrentMember.GetPersonnelQualifications();
            }
            OnPropertyChanged(nameof(PersonQualifications));

        }

        private async void OnSaveCommand()
        {
            List<string> issues = CurrentMember.GetValidationIssues();
            if (issues.Any())
            {
                string text = "There were issues saving the information: ";
                string IssueText = string.Join(',', issues.ToArray());

                text += IssueText;
                SendShortToast(text);
            }



            else
            {
                try
                {
                    await SaveCurrentPerson();
                    var toast = Toast.Make("Saved", CommunityToolkit.Maui.Core.ToastDuration.Short, 14);
                    await toast.Show(new CancellationToken());
                    var stack = Shell.Current.Navigation.NavigationStack.ToArray();

                    for (int i = stack.Length - 1; i > 0; i--)

                    {

                        Shell.Current.Navigation.RemovePage(stack[i]);

                    }
                    await Shell.Current.GoToAsync($"{nameof(CheckInOutView)}");
                    //await Shell.Current.GoToAsync($"//{nameof(CheckInOutView)}");

                }

                catch (Exception ex)
                {
                    _logger.Log(Microsoft.Extensions.Logging.LogLevel.Error, ex.ToString());

                    var toast = Toast.Make("ERROR, personnel was not saved", CommunityToolkit.Maui.Core.ToastDuration.Short, 14);
                    await toast.Show(new CancellationToken());
                }

            }
        }

        private async Task SaveCurrentPerson()
        {
            if (CurrentMember != null)
            {
                foreach (Qualification q in PersonQualifications)
                {
                    CurrentMember.QualificationList[q.QualificationListIndex] = q.PersonHas;
                }
                try
                {

                    await _personnelService.UpsertItemAsync(CurrentMember);

                }
                catch (Exception ex)
                {
                    throw ex;// new Exception("ERROR, personnel was not saved");

                }
                   
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
