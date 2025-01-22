using CommunityToolkit.Mvvm.ComponentModel;
using MySARAssist.Services;
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
        public List<IncidentItemType> AllIncidentItemTypes { get { return IncidentItemTools.GetItemTypes(true); } }
        public int FilterItemTypeId { get; set; } = 0;

        private List<IncidentInfoItemViewModel>? _items;
        public List<IncidentInfoItemViewModel>? items { get => _items; set => _items = value; }
        public string? SearchText { get => _searchText; set { _searchText = value; } }

        private string? _searchText;

        public IncidentInfoListViewModel()
        {

        }

        private async Task BuildItemList()
        {
            List<IncidentInfoItemViewModel> allItems = await new IncidentInfoService().GetAllIncidentInfoVMs();
            //apply filters
            if (FilterItemTypeId != 0)
            {
                IncidentItemType? filterType = IncidentItemTools.GetItemTypes(true).FirstOrDefault(o => o.Id == FilterItemTypeId);
                if (filterType != null)
                {
                    allItems = allItems.Where(o => o.ItemType.Equals(filterType.Name, StringComparison.InvariantCultureIgnoreCase)).ToList();
                }
            }
            //search text



            items = allItems.OrderByDescending(o => o.DateCreated).ToList();
            OnPropertyChanged(nameof(items));

        }


    }
}

