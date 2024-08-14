using MySarAssistModels.RADeMS;
using MySARAssist.Views.RADeMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySARAssist.ViewModels.RADeMS
{
    public class RADeMSCategoryViewModel : RADeMSCategory
    {

        public RADeMSCategoryViewModel()
        {
            NavigateToDetailsCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync($"{nameof(RADeMSDetailsPage)}?RademsTypeIDStr={ID}");


            });
        }

        public Command NavigateToDetailsCommand { get; }
    }
}
