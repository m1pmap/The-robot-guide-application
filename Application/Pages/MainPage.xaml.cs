using Application.Pages;
using Application.Models;
using Application.Patterns.Singleton;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace Application.Pages;

public partial class MainPage : ContentPage
{
    public ICommand SelectItemCommand { get; set; }
    public ICommand UnselectItemCommand { get; set; }
    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;
        ExhibitionsList.ItemsSource = ExhibitionManager.Instance.Items;
        CurrentItemList.ItemsSource = ExhibitionManager.Instance.CurrentItem;
        SelectItemCommand = new Command<Exhibition>(SelectItem);
        UnselectItemCommand = new Command<Exhibition>(UnelectItem);
    }
    private void SelectItem(Exhibition exhibition)
    {
        if (ExhibitionManager.Instance.CurrentItem.Count != 0)
        {
            ExhibitionManager.Instance.Items.Add(ExhibitionManager.Instance.CurrentItem[0]);
            ExhibitionManager.Instance.CurrentItem.Clear();
        }

        ExhibitionManager.Instance.Items.Remove(exhibition);
        ExhibitionManager.Instance.CurrentItem.Add(exhibition);
        CurrentItemImage.IsVisible = false;
    }

    private void UnelectItem(Exhibition exhibition)
    {
        ExhibitionManager.Instance.Items.Add(exhibition);
        ExhibitionManager.Instance.CurrentItem.Remove(exhibition);
        CurrentItemImage.IsVisible = true;
    }

    async void onClick(object sender, EventArgs e)
    {
        var name = await DisplayPromptAsync("Имя экскурсии", "Введите имя:", "OK", "Отмена");
        if (name != "" && name != null)
        {
            ExhibitionManager.Instance.Items.Add(new Exhibition { Name = name, ExhibitCount = 0, Time = 0, exhibits = new ObservableCollection<Exhibit> { } });
        }
    }

    public async void ExhibitionsList_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is Exhibition tappedExhibition)
        {
            await Navigation.PushModalAsync(new ExhibitPage(tappedExhibition));
        }
    }
}