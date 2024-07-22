using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySARAssist.Models.ValidationTools
{
    public class BaseValidationBehaviour : Behavior<Entry>
    {
        public static readonly BindableProperty ValidStyleProperty = BindableProperty.Create("ValidStyle", typeof(Style), typeof(NonBlankValidationBehavior), (Style)Application.Current.Resources["entryStyle"]);
        public static readonly BindableProperty InvalidStyleProperty = BindableProperty.Create("InvalidStyle", typeof(Style), typeof(NonBlankValidationBehavior), (Style)Application.Current.Resources["invalidEntryStyle"]);


        public Style ValidStyle
        {
            get { return (Style)GetValue(ValidStyleProperty); }
            set { SetValue(ValidStyleProperty, value); }
        }
        public Style InvalidStyle
        {
            get { return (Style)GetValue(InvalidStyleProperty); }
            set { SetValue(InvalidStyleProperty, value); }
        }

    }
}