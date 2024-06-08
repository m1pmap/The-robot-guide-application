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
using System.IO;
using System.Net.Sockets;
using System.Text;

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
                ExhibitionManager.Instance.socket = _socket;

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

    private async void StartExhibition(object sender, EventArgs e)
    {
        try
        {
            Exhibition currentExhibition = ExhibitionManager.Instance.CurrentItem[0];
            for (int i = 0; i < currentExhibition.exhibits.Count; i++)
            {
                string str = "";
                for (int j = 0; j < currentExhibition.exhibits[i].exhibitRoutes.Count; j++)
                {
                    double routeSeconds = currentExhibition.exhibits[i].exhibitRoutes[j].elapsedSeconds;
                    SendData(currentExhibition.exhibits[i].exhibitRoutes[j].Route);
                    Thread.Sleep(Convert.ToInt32(routeSeconds * 1000));
                    SendData("5");
                    Thread.Sleep(500);
                }
                SendData(currentExhibition.exhibits[i].fileName);
                while(true)
                {
                    byte[] buffer = new byte[1];
                    int bytesRead = await ExhibitionManager.Instance.socket.InputStream.ReadAsync(buffer, 0, buffer.Length);
                    char receivedChar = (char)buffer[0];
                    str = receivedChar.ToString();
                    if(str == "S")
                    {
                        break;
                    }
                }
            }
            SendData("5");
        }
        catch
        {
            await DisplayAlert("Ошибка", "Произошла ошибка", "ОК");
        }


        
    }

    private async void SendData(string message)
    {
        try
        {
            message += "\n";
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            await ExhibitionManager.Instance.socket.OutputStream.WriteAsync(buffer, 0, buffer.Length);
        }
        catch
        {
            await DisplayAlert("Ошибка", "Произошла ошибка при отправлении данных", "ОК");
            
        }
    }

    private void cancel_Clicked(object sender, EventArgs e)
    {
        SendData("C");
    }
}