using Application.Models;
using Application.Patterns.Singleton;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Xml.Linq;
using Microsoft.Maui.Controls;
using System.Xml;
using System.Security.AccessControl;

namespace Application.Pages;

public partial class Editor : ContentPage
{
    public ICommand DeleteItemCommand {  get; set; }
    public Editor()
	{
		InitializeComponent();
        BindingContext = this;
        //ExhibitionsList.ItemsSource = ExhibitionManager.Instance.Items;
        DeleteItemCommand = new Command<Exhibition>(DeleteItem);
    }

    async void onClick(object sender, EventArgs e)
    {
        string name = await DisplayPromptAsync("Создание экскурсии", "Введите название:", "OK", "Отмена");
        if(name.Length > 0) 
        {
            ExhibitionManager.Instance.Items.Add(new Exhibition { Name = name, ExhibitCount = 0, Time = 0 });
        }
    }

    private void DeleteItem(Exhibition exhibition)
    {
        ExhibitionManager.Instance.Items.Remove(exhibition);
    }

    public async void ExhibitionsList_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is Exhibition tappedExhibition)
        {
            await Navigation.PushAsync(new ExhibitPage(tappedExhibition));
        }
    }
}