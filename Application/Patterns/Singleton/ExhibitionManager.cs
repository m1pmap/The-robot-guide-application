using Android.Bluetooth;
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

        public ObservableCollection<Exhibition> Items { get; set; }
        public ObservableCollection<Exhibition> CurrentItem { get; set; }

        public BluetoothSocket socket;

        private ExhibitionManager()
        {
            Items = new ObservableCollection<Exhibition>
            {
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
