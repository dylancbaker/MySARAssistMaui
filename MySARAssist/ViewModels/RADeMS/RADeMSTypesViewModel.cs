using CommunityToolkit.Mvvm.ComponentModel;
using MySarAssistModels.RADeMS;
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
                return GetCategoryViewModels();
            }
        }

        public static List<RADeMSCategoryViewModel> GetCategoryViewModels()
        {
            List<RADeMSCategoryViewModel> categories = new List<RADeMSCategoryViewModel>();

            categories.Add(new RADeMSCategoryViewModel { ID = 1, Name = "Ground Search", Questions =RADeMSTools.GetQuestions(1) }); //(1, "Ground Search", GetQuestions(1)));
            categories.Add(new RADeMSCategoryViewModel { ID = 2, Name = "Human Disease Outbreak", Questions = RADeMSTools.GetQuestions(2) }); //(2, "Human Disease Outbreak", GetQuestions(2)));
            categories.Add(new RADeMSCategoryViewModel { ID = 3, Name = "Motor Vehicle Operations", Questions = RADeMSTools.GetQuestions(3) }); //(3, "Motor Vehicle Operations", GetQuestions(3)));
            categories.Add(new RADeMSCategoryViewModel { ID = 4, Name = "Mountain Rescue", Questions = RADeMSTools.GetQuestions(4) }); //(4, "Mountain Rescue", GetQuestions(4)));
            categories.Add(new RADeMSCategoryViewModel { ID = 5, Name = "Rope Rescue", Questions = RADeMSTools.GetQuestions(5) }); //(5, "Rope Rescue", GetQuestions(5)));
            categories.Add(new RADeMSCategoryViewModel { ID = 6, Name = "Swiftwater Rescue", Questions = RADeMSTools.GetQuestions(6) }); //(6, "Swiftwater Rescue", GetQuestions(6)));


            return categories;

        }


    }
}
