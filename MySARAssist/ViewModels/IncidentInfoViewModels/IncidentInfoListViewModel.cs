using CommunityToolkit.Mvvm.ComponentModel;
using MySarAssistModels.IncidentItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySARAssist.ViewModels.IncidentInfoViewModels
{
    public class IncidentInfoListViewModel : ObservableObject
    {
        public List<IncidentItemType> AllIncidentItemTypes
        {
            get
            {
                return IncidentItemTools.GetItemTypes(true);
            }
        }
        public int FilterItemTypeId { get; set; } = 0;

    }
}
