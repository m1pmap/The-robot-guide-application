using Application.Models;

namespace Application.Pages;

public partial class ExhibitRoutePage : ContentPage
{
	public ExhibitRoutePage(Exhibit exhibit)
	{
        BindingContext = this;
        InitializeComponent();
        ExhibitName.Text = exhibit.Name;
    }
}