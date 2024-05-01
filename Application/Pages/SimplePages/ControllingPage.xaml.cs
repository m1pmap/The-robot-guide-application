using Android.Bluetooth;
using Org.W3c.Dom;

namespace Application.Pages.SimplePages;

public partial class ControllingPage : ContentPage
{
    BluetoothSocket socket;
    byte[] buffer = new byte[1];

    public ControllingPage(BluetoothSocket _socket)
	{
		InitializeComponent();
        socket = _socket;
    }
    private async void Forward_button(object sender, EventArgs e)
    {
        SendData('1', socket);
    }

    private async void Back_button(object sender, EventArgs e)
    {
        SendData('2', socket);
    }

    private async void Left_button(object sender, EventArgs e)
    {
        SendData('3', socket);
    }

    private async void Right_button(object sender, EventArgs e)
    {
        
        SendData('4', socket);
    }


    private async void SendData(char data, BluetoothSocket socket)
    {
        buffer[0] = (byte)data;
        await socket.OutputStream.WriteAsync(buffer, 0, buffer.Length);
    }

}