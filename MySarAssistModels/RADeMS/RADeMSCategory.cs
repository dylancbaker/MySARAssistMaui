using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySarAssistModels.RADeMS
{
    public class RADeMSCategory
    {
        public RADeMSCategory()
        {
            
        }
        private int _ID;
        private string _Name = string.Empty;

        private List<RADeMSQuestion>? _Questions = new List<RADeMSQuestion>();
        public int ID { get { return _ID; } set => _ID = value; }
        public string Name { get { return _Name; } set => _Name = value; }
        public List<RADeMSQuestion>? Questions { get => _Questions; set => _Questions = value; }
        
        public RADeMSCategory(int id, string name, List<RADeMSQuestion> questions) { _ID = id; _Name = name; Questions = questions; }
    }
}
