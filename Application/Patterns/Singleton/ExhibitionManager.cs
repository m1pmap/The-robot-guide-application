using Application.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Patterns.Singleton
{
    internal class ExhibitionManager
    {
        private static ExhibitionManager instance;

        public ObservableCollection<Exhibition> Items { get; private set; }
        public ObservableCollection<Exhibition> CurrentItem { get; private set; }

        private ExhibitionManager()
        {
            Items = new ObservableCollection<Exhibition>
            {
                new Exhibition {Name = "До современности", ExhibitCount = 0, Time = 0, exhibits = new ObservableCollection<Exhibit> { } },
                new Exhibition {Name = "Исторический музей в Москве", ExhibitCount = 0, Time = 0, exhibits = new ObservableCollection<Exhibit> { } }
            };

            CurrentItem = new ObservableCollection<Exhibition> { };
        }

        public static ExhibitionManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ExhibitionManager();
                }
                return instance;
            }
        }
    }
}
