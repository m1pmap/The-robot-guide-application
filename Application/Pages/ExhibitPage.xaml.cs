using Application.Models;
using Application.Patterns.Singleton;
using Microcharts;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.Net.Sockets;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Controls.Xaml;
using System.Windows.Input;
using Android.Bluetooth;
using Java.Net;
using System.Text;

namespace Application.Pages;

public partial class ExhibitPage : ContentPage
{
    public int pressedCount = 0; // Кол-во нажатий на кнопку появления / скрытия кнопок управления роботом
    Exhibition thisExhibition; //Текущая экскурсия
    List<string> colors = new List<string> { "#c3426b", "#dc724a", "#33986a", "#80c442" }; // Всевозможные цвета экспонатов
    int currentColourIndex = 100;
    int currentImageIndex = 1;
    MyTimer timer = new MyTimer();
    ObservableCollection<ExhibitRoute> routes = new ObservableCollection<ExhibitRoute> { };
    public ICommand DeleteItemCommand { get; set; }

    public ObservableCollection<Exhibit> Items { get; private set; }
    public ExhibitPage(Exhibition exhibition) //Конструктор
    {
        BindingContext = this;
        InitializeComponent();

        thisExhibition = exhibition;
        ExhibitionName.Text = exhibition.Name;

        ExhibitsList.ItemsSource = thisExhibition.exhibits;
        currentColourIndex = getRandomColorIndex(currentColourIndex);
        AddButton.Background = Microsoft.Maui.Graphics.Color.FromRgba(colors[currentColourIndex]);

        DeleteItemCommand = new Command<Exhibit>(DeleteItem);
    }

    private int getRandomColorIndex(int previousColour) // возвращает случайный индекс цвета экскурсии, в зависимости от предыдущего
    {
        while (true)
        {
            Random rand = new Random();
            int currentColour = rand.Next(colors.Count);
            if (previousColour != currentColour)
            {
                return currentColour;
            }
        }
    }

    private void ShowHideConroller(object sender, EventArgs e) //Показ и скрытие кнопок управления роботом
    {
        if (pressedCount == 0)
        {
            Forward_button.IsVisible = true;
            Left_button.IsVisible = true;
            Right_button.IsVisible = true;
            Back_button.IsVisible = true;
            pressedCount = 1;
        }
        else
        {
            Forward_button.IsVisible = false;
            Left_button.IsVisible = false;
            Right_button.IsVisible = false;
            Back_button.IsVisible = false;
            pressedCount = 0;
        }

    }

    async private void AddExhibit(object sender, EventArgs e) //Добавление экскурсии
    {
        double exhibitTime = 0;
        var name = await DisplayPromptAsync("Название экспоната", "Введите название:", "OK", "Отмена");
        if (name != null && name != "" && routes.Count != 0)
        {
            ObservableCollection<ExhibitRoute> newExhibitRoutes = new ObservableCollection<ExhibitRoute> { };
            for(int i = 0; i < routes.Count; i++)
            {
                newExhibitRoutes.Add(routes[i]);
                exhibitTime += routes[i].elapsedSeconds;
            }
            thisExhibition.exhibits.Add(new Exhibit { Name = name, Color = colors[currentColourIndex], exhibitRoutes = newExhibitRoutes, fileName = ""});
            routes.Clear();
            thisExhibition.ExhibitCount++;
            thisExhibition.Time += Math.Round(exhibitTime / 60, 2);
        }
        currentColourIndex = getRandomColorIndex(currentColourIndex);
        AddButton.Background = Microsoft.Maui.Graphics.Color.FromRgba(colors[currentColourIndex]);
    }

    private void Forward_buttonPressed(object sender, EventArgs e)
    {
        timer.Start();
        routes.Add(new ExhibitRoute { Route = "1", source = "up", color = colors[currentColourIndex]});
        SendData("1");
    }
    private void Left_buttonPressed(object sender, EventArgs e)
    {
        timer.Start();
        routes.Add(new ExhibitRoute { Route = "3", source = "left", color = colors[currentColourIndex] });
        SendData("3");
    }
    private void Right_buttonPressed(object sender, EventArgs e)
    {
        timer.Start();
        routes.Add(new ExhibitRoute { Route = "4", source = "right", color = colors[currentColourIndex] });
        SendData("4");
    }
    private void Back_buttonPressed(object sender, EventArgs e)
    {
        timer.Start();
        routes.Add(new ExhibitRoute { Route = "2", source = "down", color = colors[currentColourIndex] });
        SendData("2");
    }

    private void ButtonsReleased(object sender, EventArgs e)
    {
        timer.Stop();
        double elapsedSeconds = Math.Round(timer.GetElapsedSeconds(), 3);
        if (elapsedSeconds > 0)
        {
            routes[routes.Count - 1].elapsedSeconds = elapsedSeconds;
        }
        SendData("5");
    }

    private void DeleteItem(Exhibit exhibit)
    {
        thisExhibition.exhibits.Remove(exhibit);
        thisExhibition.ExhibitCount--;
    }

    public async void ExhibitList_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is Exhibit tappedExhibit)
        {
            await Navigation.PushModalAsync(new ExhibitRoutePage(tappedExhibit));
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

    private async void EditButton(object sender, EventArgs e)
    {
        var name = await DisplayPromptAsync("Новое название экскурсии", "Введите название:", "OK", "Отмена");
        if (name != null && name != "")
        {
            thisExhibition.Name = name;
            ExhibitionName.Text = name;
            ObservableCollection<Exhibition> exhibitions = new ObservableCollection<Exhibition>();
            foreach (Exhibition exhibition in ExhibitionManager.Instance.Items)
            {
                exhibitions.Add(exhibition);
            }
            ExhibitionManager.Instance.Items.Clear();
            foreach (Exhibition exhibition in exhibitions)
            {
                ExhibitionManager.Instance.Items.Add(exhibition);
            }
        }
    }

    private async void DeleteExhibition(object sender, EventArgs e)
    {
        bool result = await DisplayAlert("Подтверждение", "Вы уверены, что хотите продолжить?", "OK", "Отмена");
        if (result)
        {
            ExhibitionManager.Instance.Items.Remove(thisExhibition);
            await Navigation.PopModalAsync();
        }
    }
}