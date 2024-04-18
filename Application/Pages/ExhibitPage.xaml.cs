using Application.Models;

namespace Application.Pages;

public partial class ExhibitPage : ContentPage
{
	public ExhibitPage(Exhibition exhibition)
	{
		InitializeComponent();
		ExhibitName.Text = exhibition.Name;
	}
}