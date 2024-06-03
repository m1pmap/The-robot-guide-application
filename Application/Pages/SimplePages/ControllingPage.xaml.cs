using Android.Bluetooth;
using Application.Patterns.Singleton;
using System.Text;

namespace Application.Pages.SimplePages;

public partial class ControllingPage : ContentPage
{
    BluetoothSocket socket;

    public ControllingPage(BluetoothSocket _socket)
    {
        InitializeComponent();
        socket = _socket;
    }
    private void Forward_buttonPressed(object sender, EventArgs e)
    {
        SendData("1");
    }

    private void Back_buttonPressed(object sender, EventArgs e)
    {
        SendData("2");
    }

    private void Left_buttonPressed(object sender, EventArgs e)
    {
        SendData("3");
    }

    private void Right_buttonPressed(object sender, EventArgs e)
    {
        SendData("4");
    }

    private void ButtonsReleased(object sender, EventArgs e)
    {
        SendData("5");
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
}