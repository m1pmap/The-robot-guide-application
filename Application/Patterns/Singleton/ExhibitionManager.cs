using Application.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                new Exhibition {Name = "До современности", ExhibitCount = 11, Time = 25, exhibits = new ObservableCollection<Exhibit> { new Exhibit { Name = "Мамонт пригорья", Color = "#C3426B", Source = "start.png" }} },
                new Exhibition {Name = "Исторический музей в Москве", ExhibitCount = 5, Time = 10, exhibits = new ObservableCollection<Exhibit> { new Exhibit { Name = "Человек c костями", Color = "#DC724A", Source = "start.png" } } }
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
