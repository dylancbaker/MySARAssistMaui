using MySarAssistModels.RADeMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySARAssist.Models.Events
{
    public delegate void RADeMSEventHandler(RADeMSEventArgs e);

    public class RADeMSEventArgs
    {
        public RADeMSQuestion question { get; set; }
        public int SelectedValue { get; set; }
        public RADeMSEventArgs(RADeMSQuestion q, int selected_value) { question = q; SelectedValue = selected_value; }
    }
}
