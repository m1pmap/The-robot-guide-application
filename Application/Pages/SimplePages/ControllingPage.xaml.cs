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
    private void Forward_buttonPressed(object sender, EventArgs e)
    {
        SendData('1', socket);
    }

    private void Back_buttonPressed(object sender, EventArgs e)
    {
        SendData('2', socket);
    }

    private void Left_buttonPressed(object sender, EventArgs e)
    {
        SendData('3', socket);
    }

    private void Right_buttonPressed(object sender, EventArgs e)
    {
        SendData('4', socket);
    }

    private void ButtonsReleased(object sender, EventArgs e)
    {
        SendData('5', socket);
    }


    private async void SendData(char data, BluetoothSocket socket)
    {
        try
        {
            buffer[0] = (byte)data;
            await socket.OutputStream.WriteAsync(buffer, 0, buffer.Length);
        }
        catch
        {
            await DisplayAlert("Ошибка", "Произошла ошибка при отправлении данных", "ОК");
        }
    }
}