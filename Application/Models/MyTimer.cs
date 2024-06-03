using System;
using System.Timers;

namespace Application.Models
{
    public class MyTimer
    {
        private System.Timers.Timer timer; 
        private DateTime startTime;
        public TimeSpan elapsedTime;

        public MyTimer()
        {
            timer = new System.Timers.Timer(10); // Интервал таймера срабатывания таймера
            timer.Elapsed += Timer_Elapsed; // установка метода для таймера
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e) // Метод, который будет срабатывать каждые 10 миллисекунд
        {
            elapsedTime = DateTime.Now - startTime; // поиск пройденного времени
        }

        public void Start() // запуск таймера
        {
            startTime = DateTime.Now; // установка начала таймера в текущее время
            timer.Start();
        }

        public void Stop() // остановка таймера
        {
            timer.Stop();
        }

        public double GetElapsedSeconds() // пройденное время в секундах
        {
            return elapsedTime.TotalSeconds;
        }
    }
}
