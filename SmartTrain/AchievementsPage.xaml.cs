using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace SmartTrain
{
    // Допоміжний клас-модель для відображення кожної картки в списку
    public class AchievementViewItem
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsUnlocked { get; set; }

        // Якщо відкрито - кубок, якщо ні - порожній рядок (невидимий символ)
        public string Icon => IsUnlocked ? "🏆" : " ";

        // Відкриті досягнення яскраві, заблоковані - напівпрозорі (50%)
        public double CardOpacity => IsUnlocked ? 1.0 : 0.5;

        // Текст статусу
        public string StatusText => IsUnlocked ? "Відкрито" : "Заблоковано";

        // Іконка статусу (Галочка або Замочок)
        public string StatusGlyph => IsUnlocked ? "\xE73E" : "\xE72E";

        // Колір статусу (Зелений або Сірий)
        public SolidColorBrush StatusColorBrush => IsUnlocked
            ? new SolidColorBrush(Windows.UI.Color.FromArgb(255, 144, 238, 144)) // LightGreen
            : new SolidColorBrush(Windows.UI.Color.FromArgb(255, 128, 128, 128)); // Gray
    }

    public sealed partial class AchievementsPage : Page
    {
        private string profilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "user_profile.json");

        public AchievementsPage()
        {
            this.InitializeComponent();
            this.Loaded += AchievementsPage_Loaded;
        }

        private void AchievementsPage_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAchievementsData();
        }

        private void LoadAchievementsData()
        {
            // 1. Читаємо актуальний профіль користувача
            UserProfile currentUser = new UserProfile();
            if (File.Exists(profilePath))
            {
                string json = File.ReadAllText(profilePath);
                currentUser = JsonSerializer.Deserialize<UserProfile>(json) ?? new UserProfile();
            }

            // 2. Формуємо список для екрану
            List<AchievementViewItem> displayList = new List<AchievementViewItem>();

            // Проходимося по всій базі з 25 досягнень, яку ми створили раніше
            foreach (var ach in AchievementManager.AllAchievements)
            {
                // Перевіряємо, чи є ID цього досягнення у списку відкритих
                bool unlocked = currentUser.UnlockedAchievements.Contains(ach.Id);

                displayList.Add(new AchievementViewItem
                {
                    Title = ach.Title,
                    Description = ach.Description,
                    IsUnlocked = unlocked
                });
            }

            // 3. Відправляємо дані в ListView
            AchievementsList.ItemsSource = displayList;
        }
    }
}