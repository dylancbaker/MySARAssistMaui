using CommunityToolkit.Mvvm.ComponentModel;
using MySARAssist.Models.RADeMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySARAssist.ViewModels.RADeMS
{
    public class RADeMSTypesViewModel : ObservableObject
    {
        public RADeMSTypesViewModel()
        {
            
        }

        public List<RADeMSCategoryViewModel> RADeMSCategories
        {
            get
            {
                return RADeMSTools.GetCategoryViewModels();
            }
        }
    }
}
