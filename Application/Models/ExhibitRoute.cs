using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ExhibitRoute
    {
        public char Route { get; set; }
        public string source {  get; set; }
        public double elapsedSeconds { get; set; }
        public string color { get; set; }
    }
}
