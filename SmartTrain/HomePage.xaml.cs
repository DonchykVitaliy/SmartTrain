using Microsoft.UI.Xaml.Controls;
using System;
using System.IO;
using System.Text.Json;

namespace SmartTrain
{
    public sealed partial class HomePage : Page
    {
        private string profilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "user_profile.json");

        // масив мотиваційних фраз
        private string[] motivations = new string[]
        {
            "Готовий стати кращою версією себе сьогодні? Твій план вже чекає!",
            "Дисципліна — це міст між цілями та досягненнями. Почнемо?",
            "Не чекай ідеального моменту, бери цей момент і роби його ідеальним!",
            "Кожне тренування робить тебе на крок ближчим до мрії. Не зупиняйся!",
            "Твоє тіло може все. Головне — переконати в цьому свій розум!"
        };

        public HomePage()
        {
            this.InitializeComponent();
            LoadDashboard();
        }

        private void LoadDashboard()
        {
            // сьогоднішня дата
            DateText.Text = DateTime.Now.ToString("dd MMMM yyyy, dddd");

            // мотивація
            Random rnd = new Random();
            MotivationText.Text = motivations[rnd.Next(motivations.Length)];

            // профіль та вогник
            if (File.Exists(profilePath))
            {
                string json = File.ReadAllText(profilePath);
                var user = JsonSerializer.Deserialize<UserProfile>(json) ?? new UserProfile();

                // різниця між минулим днем та сьогодні
                TimeSpan timeSinceLastWorkout = DateTime.Now.Date - user.LastWorkoutDate.Date;

                // пройшло БІЛЬШЕ 1 дня
                if (timeSinceLastWorkout.Days > 1 && user.CurrentStreak > 0)
                {
                    user.CurrentStreak = 0;

                    // запис скинутого вогника
                    string updatedJson = JsonSerializer.Serialize(user, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(profilePath, updatedJson);
                }

                // актуальні дані вивод на екран
                GreetingText.Text = $"Привіт, {user.UserName}!";
                CurrentStreakText.Text = user.CurrentStreak.ToString();
                RecordStreakText.Text = $"Рекорд: {user.RecordStreak}";
            }
            else
            {
                GreetingText.Text = "Привіт, Атлете!";
                MotivationText.Text = "Створи профіль, щоб розпочати свій шлях!";
            }
        }
    }
}