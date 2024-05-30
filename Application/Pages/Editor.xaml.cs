using Application.Models;
using Application.Patterns.Singleton;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Xml.Linq;
using Microsoft.Maui.Controls;
using System.Xml;
using System.Security.AccessControl;
using Android.Bluetooth;
using Java.Util;
using Application.Pages.SimplePages;
using System.Threading;

namespace Application.Pages;

public partial class Editor : ContentPage
{
    BluetoothSocket _socket;
    public Editor()
	{
		InitializeComponent();
        BindingContext = this;
    }

    private async void Connection_button(object sender, EventArgs e)
    {     
        Connecting();
    }

    private async void Connecting()
    {
        activityIndicator.IsRunning = true;
        try
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
            {
                BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
                if (adapter == null)
                    throw new Exception("Bluetooth-адаптер не найден");

                if (!adapter.IsEnabled)
                    throw new Exception("Адаптер Bluetooth не включен");

                BluetoothDevice device = (from bd in adapter.BondedDevices where bd.Name == "HC-05" select bd).FirstOrDefault();

                if (device == null)
                    throw new Exception("Именованное устройство не найдено");

                _socket = device.CreateRfcommSocketToServiceRecord(UUID.FromString("00001101-0000-1000-8000-00805f9b34fb"));
                await _socket.ConnectAsync();

                activityIndicator.IsRunning = false;
                await DisplayAlert("Подключение успешно", "Вы подключены", "ОК");
            }
        }
        catch (Exception ex)
        {
            activityIndicator.IsRunning = false;
            await DisplayAlert("Ошибка подключения", "Произошла ошибка подключение, проверьте ваши настройки. Возможно вы уже были подключены", "ОК");
        }
    }

    private async void Controlling_button(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new ControllingPage(_socket));
    }

    private void StartExhibition(object sender, EventArgs e)
    {
        Exhibition currentExhibition = ExhibitionManager.Instance.CurrentItem[0];
        for (int i = 0; i < currentExhibition.exhibits.Count; i++)
        {
            for (int j = 0; j < currentExhibition.exhibits[i].exhibitRoutes.Count; j++)
            {
                double routeSeconds = currentExhibition.exhibits[i].exhibitRoutes[j].elapsedSeconds;
                start.Text = currentExhibition.exhibits[i].exhibitRoutes[j].Route.ToString();
                Thread.Sleep(Convert.ToInt32(routeSeconds * 1000));
            }
        }
    }
}