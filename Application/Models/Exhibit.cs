using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class Exhibit
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public ObservableCollection<ExhibitRoute> exhibitRoutes { get; set; }
    }
}
