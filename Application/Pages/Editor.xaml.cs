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
        var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
        if (status != PermissionStatus.Granted)
        {
            BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
            if (adapter == null)
                throw new Exception("No Bluetooth adapter found.");

            if (!adapter.IsEnabled)
                throw new Exception("Bluetooth adapter is not enabled.");

            BluetoothDevice device = (from bd in adapter.BondedDevices where bd.Name == "HC-05" select bd).FirstOrDefault();

            if (device == null)
                throw new Exception("Named device not found.");

            _socket = device.CreateRfcommSocketToServiceRecord(UUID.FromString("00001101-0000-1000-8000-00805f9b34fb"));
            await _socket.ConnectAsync();

            //await _socket.InputStream.ReadAsync(buffer, 0, buffer.Length);
        }
    }

    private async void Controlling_button(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new ControllingPage(_socket));
    }
}