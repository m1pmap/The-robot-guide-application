using Application.Models;
using Application.Patterns.Singleton;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace Application
{
    public partial class App
    {
        private string exhibitionsFilePath = Path.Combine(FileSystem.AppDataDirectory, "exhibition.json");
        private string currentExhibitionFilePath = Path.Combine(FileSystem.AppDataDirectory, "currentExhibition.json");
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            LoadData();
        }

        protected override void OnSleep()
        {
            SaveData();
        }

        private void SaveData()
        {
            var jsonexhibitionsString = JsonSerializer.Serialize(ExhibitionManager.Instance.Items);
            File.WriteAllText(exhibitionsFilePath, jsonexhibitionsString);
            var jsonCurrentExhibitionString = JsonSerializer.Serialize(ExhibitionManager.Instance.CurrentItem);
            File.WriteAllText(currentExhibitionFilePath, jsonCurrentExhibitionString);
        }

        private void LoadData()
        {
            if (File.Exists(exhibitionsFilePath)) //загрузка неактивных экскурсий
            {
                ExhibitionManager.Instance.Items.Clear();
                var jsonString = File.ReadAllText(exhibitionsFilePath);
                var exhibitions = JsonSerializer.Deserialize<ObservableCollection<Exhibition>>(jsonString);

                foreach (var exhibition in exhibitions)
                {
                    ExhibitionManager.Instance.Items.Add(exhibition);
                }
            }

            if (File.Exists(currentExhibitionFilePath)) //загрузка текущей экскурсии
            {
                ExhibitionManager.Instance.CurrentItem.Clear();
                var jsonString = File.ReadAllText(currentExhibitionFilePath);
                var exhibitions = JsonSerializer.Deserialize<ObservableCollection<Exhibition>>(jsonString);

                foreach (var exhibition in exhibitions)
                {
                    ExhibitionManager.Instance.Items.Add(exhibition);
                }
            }
        }
    }
}
