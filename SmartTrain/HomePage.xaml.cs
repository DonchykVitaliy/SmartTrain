using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SmartTrain;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class HomePage : Page
{
    public HomePage()
    {
        this.InitializeComponent();
        LoadDashboard();
    }

    private void LoadDashboard()
    {
        // 1. Встановлюємо сьогоднішню дату
        DateText.Text = DateTime.Now.ToString("dd MMMM yyyy, dddd");

        // 2. Читаємо профіль користувача, щоб взяти ім'я та серію
        string profilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "user_profile.json");

        if (File.Exists(profilePath))
        {
            string json = File.ReadAllText(profilePath);
            var user = JsonSerializer.Deserialize<UserProfile>(json) ?? new UserProfile();

            // Вставляємо дані в інтерфейс
            GreetingText.Text = $"Привіт, {user.UserName}!";
            CurrentStreakText.Text = user.CurrentStreak.ToString();
            RecordStreakText.Text = $"Рекорд серії: {user.RecordStreak}";
        }
        else
        {
            GreetingText.Text = "Привіт, Атлете!";
        }
    }
}
