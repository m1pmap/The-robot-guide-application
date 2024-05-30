using Application.Models;
using System.Collections.ObjectModel;

namespace Application.Pages;

public partial class ExhibitRoutePage : ContentPage
{
	public ExhibitRoutePage(Exhibit exhibit)
	{
        BindingContext = this;
        InitializeComponent();
        ExhibitName.Text = exhibit.Name;

        ObservableCollection<ExhibitRoute> Items = new ObservableCollection<ExhibitRoute>
            {
                new ExhibitRoute {Route = '1',  source = "up", elapsedSeconds = 12.22},
                new ExhibitRoute {Route = '1', source = "up", elapsedSeconds = 22.22}
            };

        ExhibitRouteList.ItemsSource = exhibit.exhibitRoutes;
    }
}