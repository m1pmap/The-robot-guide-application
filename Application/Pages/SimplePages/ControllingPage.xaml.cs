using Android.Bluetooth;

namespace Application.Pages.SimplePages;

public partial class ControllingPage : ContentPage
{
    BluetoothSocket socket;
    public ControllingPage(BluetoothSocket _socket)
	{
		InitializeComponent();
        socket = _socket;
    }
    private async void Forward_button(object sender, EventArgs e)
    {
        byte[] buffer = new byte[1];
        buffer[0] = (byte)'1';
        // Write data to the device
        await socket.OutputStream.WriteAsync(buffer, 0, buffer.Length);
    }

    private async void Back_button(object sender, EventArgs e)
    {
        byte[] buffer = new byte[1];
        buffer[0] = (byte)'2';
        // Write data to the device
        await socket.OutputStream.WriteAsync(buffer, 0, buffer.Length);
    }
}