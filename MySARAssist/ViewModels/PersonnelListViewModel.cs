using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MySARAssist.ViewModels
{
    internal class PersonnelListViewModel : ObservableObject
    {
        private string dbPath = string.Empty;

        public string DbPath { get => dbPath; set => SetProperty(ref dbPath, value); }
    }
}
