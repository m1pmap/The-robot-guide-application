using Application.Models;
using Application.Patterns.Singleton;
using Microcharts;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.Net.Sockets;

namespace Application.Pages;

public partial class ExhibitPage : ContentPage
{
    public int pressedCount = 0; // Кол-во нажатий на кнопку появления / скрытия кнопок управления роботом
    Exhibition thisExhibition; //Текущая экскурсия
    List<string> colors = new List<string> { "#c3426b", "#dc724a", "#33986a", "#80c442" }; // Всевозможные цвета экспонатов
    List<string> images = new List<string> { "start", "one", "two", "three", "four", "five", "six", "seven", "eight" };
    int currentColourIndex = 100;
    int currentImageIndex = 1;
    MyTimer timer = new MyTimer();

    public ObservableCollection<Exhibit> Items { get; private set; }
    public ExhibitPage(Exhibition exhibition) //Конструктор
    {
        thisExhibition = exhibition;
        InitializeComponent();
        ExhibitName.Text = exhibition.Name;
        ExhibitsList.ItemsSource = thisExhibition.exhibits;
        currentColourIndex = getRandomColorIndex(currentColourIndex);
        AddButton.Background = Microsoft.Maui.Graphics.Color.FromRgba(colors[currentColourIndex]);
    }

    private int getRandomColorIndex(int previousColour) // возвращает случайный индекс цвета экскурсии, в зависимости от предыдущего
    {
        while (true) 
        {
            Random rand = new Random();
            int currentColour = rand.Next(colors.Count);
            if(previousColour != currentColour)
            {
                return currentColour;
            }
        }
    }

    private void ShowHideConroller(object sender, EventArgs e) //Показ и скрытие кнопок управления роботом
    {
        if(pressedCount == 0)
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
        var name = await DisplayPromptAsync("Название экспоната", "Введите название:", "OK", "Отмена");
        if (name != null && name != "")
        {
            if(thisExhibition.exhibits.Count == 0)
            {
                currentImageIndex = 0;
            }
            else
            {
                if(currentImageIndex == 8) 
                {
                    currentImageIndex = 1;
                }
                else
                {
                    currentImageIndex++;
                }
            }
            thisExhibition.exhibits.Add(new Exhibit { Name = name, Color = colors[currentColourIndex], Source = images[currentImageIndex] });
        }
        currentColourIndex = getRandomColorIndex(currentColourIndex);
        AddButton.Background = Microsoft.Maui.Graphics.Color.FromRgba(colors[currentColourIndex]);
    }

    private void Forward_buttonPressed(object sender, EventArgs e)
    {
        timer.Start();
    }
    private void Left_buttonPressed(object sender, EventArgs e)
    {
        timer.Start();
    }
    private void Right_buttonPressed(object sender, EventArgs e)
    {
        timer.Start();
    }
    private void Back_buttonPressed(object sender, EventArgs e)
    {
        timer.Start();
    }

    private void ButtonsReleased(object sender, EventArgs e)
    {
        timer.Stop();
        ExhibitName.Text = Math.Round(timer.GetElapsedSeconds(), 3).ToString();
    }
}