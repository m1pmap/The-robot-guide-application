using Application.Models;
using System.Collections.ObjectModel;

namespace Application.Pages;

public partial class ExhibitRoutePage : ContentPage
{
    Exhibit currentExhibit = new Exhibit();
	public ExhibitRoutePage(Exhibit exhibit)
	{
        currentExhibit = exhibit;
        BindingContext = this;
        InitializeComponent();
        ExhibitName.Text = exhibit.Name;
        FileName.Text = "Имя файла: ";
        if(exhibit.fileName != "") 
        {
            FileName.Text += exhibit.fileName;
        }
        else
        {
            FileName.Text += "-";
        }
        ExhibitRouteList.ItemsSource = exhibit.exhibitRoutes;
    }

    private async void AddFileName(object sender, EventArgs e)
    {
        var name = await DisplayPromptAsync("Название файла", "Введите название:", "OK", "Отмена");
        if (name != null && name != "")
        {
            currentExhibit.fileName = name;
            FileName.Text = "Имя файла: " + name + ".wav";
        }
    }
}