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
    // клас для відображення карток
    public class AchievementViewItem
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsUnlocked { get; set; }

        // відкрито = кубок, якщо ні = нічого
        public string Icon => IsUnlocked ? "🏆" : " ";

        //відкриті яскраві, заблоковані - напівпрозорі
        public double CardOpacity => IsUnlocked ? 1.0 : 0.5;

        public string StatusText => IsUnlocked ? "Відкрито" : "Заблоковано";

        // галочка або замок статусу
        public string StatusGlyph => IsUnlocked ? "\xE73E" : "\xE72E";

        // колір статусу (зелений або сірий)
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
            // читка профілю користувача
            UserProfile currentUser = new UserProfile();
            if (File.Exists(profilePath))
            {
                string json = File.ReadAllText(profilePath);
                currentUser = JsonSerializer.Deserialize<UserProfile>(json) ?? new UserProfile();
            }

            // формуємо список для сторінки
            List<AchievementViewItem> displayList = new List<AchievementViewItem>();

            // перевірка всіє бази досягнень
            foreach (var ach in AchievementManager.AllAchievements)
            {
                //чи є ID цього досягнення у списку відкритих
                bool unlocked = currentUser.UnlockedAchievements.Contains(ach.Id);

                displayList.Add(new AchievementViewItem
                {
                    Title = ach.Title,
                    Description = ach.Description,
                    IsUnlocked = unlocked
                });
            }

            //всі дані в ListView
            AchievementsList.ItemsSource = displayList;
        }
    }
}