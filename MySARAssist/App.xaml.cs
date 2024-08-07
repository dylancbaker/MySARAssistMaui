﻿/* Unmerged change from project 'MySARAssist (net8.0-ios)'
Added:
using MySARAssist.Models.Personnel;
using MySARAssist.Models.Personnel.Personnel;
using MySARAssist.Models.Personnel.Personnel.Personnel;
*/

/* Unmerged change from project 'MySARAssist (net8.0-android)'
Added:
using MySARAssist.Models.Personnel;
using MySARAssist.Models.Personnel.Personnel;
*/

/* Unmerged change from project 'MySARAssist (net8.0-windows10.0.19041.0)'
Added:
using MySARAssist.Models.Personnel;
*/
using MySARAssist.Interfaces;
using MySARAssist.Models.People;
using MySARAssist.Services;

namespace MySARAssist
{
    public partial class App : Application
    {
        public static Personnel? CurrentPerson { get; set; }


        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            LoadCurrentPerson();

        }

        private async void LoadCurrentPerson()
        {
            CurrentPerson = await new PersonnelService().GetCurrentPersonAsync();
            if(CurrentPerson == null)
            {
                ;
            }
        }
    }
}
