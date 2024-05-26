using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class Exhibition
    {
        public string Name { get; set; }
        public int ExhibitCount { get; set; }
        public int Time { get; set; }
        public ObservableCollection<Exhibit> exhibits { get; set; }
    }
}
