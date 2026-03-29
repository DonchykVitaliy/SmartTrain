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
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SmartTrain
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private string profilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "user_profile.json");
        // Для об'єкта користувача
        public UserProfile CurrentUser { get; set; } = default!;

        public MainWindow()
        {
            this.InitializeComponent();
            CheckUserProfile();
        }

        private void CheckUserProfile()
        {
            if (File.Exists(profilePath))
            {
                // Якщо файл є — завантажуємо його
                string json = File.ReadAllText(profilePath);
                CurrentUser = JsonSerializer.Deserialize<UserProfile>(json);

                // Показуємо головне меню та ім'я користувача
                ShowMainApp();
            }
            else
            {
                // Якщо файлу немає — приховуємо меню і показуємо форму реєстрації
                MainNav.Visibility = Visibility.Collapsed;
                ShowRegistrationForm();
            }
        }

        private void ShowMainApp()
        {
            MainNav.Visibility = Visibility.Visible;
            // Можна змінити заголовок меню на ім'я користувача
            MainNav.PaneTitle = $"Профіль: {CurrentUser.UserName}";


            // Автоматично вибираємо перший пункт (Головна сторінка)
            MainNav.SelectedItem = MainNav.MenuItems[0];
            ContentFrame.Navigate(typeof(HomePage));
        }

        private void SaveProfile_Click(object sender, RoutedEventArgs e)
        {
            // Створюємо об'єкт з введених даних
            CurrentUser = new UserProfile
            {
                UserName = RegName.Text,
                Age = (int)RegAge.Value,
                Weight = RegWeight.Value,
                Height = RegHeight.Value,
                FitnessLevel = (RegLevel.SelectedItem as ComboBoxItem)?.Content.ToString()
            };

            // Серіалізуємо в JSON
            string json = JsonSerializer.Serialize(CurrentUser, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(profilePath, json);

            // Ховаємо реєстрацію, показуємо додаток
            RegistrationArea.Visibility = Visibility.Collapsed;
            ShowMainApp();
        }

        private void ShowRegistrationForm()
        {
            RegistrationArea.Visibility = Visibility.Visible;
        }

        // 1. Відкриття діалогу з поточними даними
        private async void ProfileItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            EditProfileDialog.XamlRoot = this.Content.XamlRoot; // Необхідно для WinUI 3

            // Заповнюємо поля поточними даними
            EditName.Text = CurrentUser.UserName;
            EditAge.Value = CurrentUser.Age;
            EditWeight.Value = CurrentUser.Weight;
            EditHeight.Value = CurrentUser.Height;

            // Встановлюємо правильний індекс у ComboBox
            foreach (ComboBoxItem item in EditLevel.Items)
            {
                if (item.Content.ToString() == CurrentUser.FitnessLevel)
                {
                    EditLevel.SelectedItem = item;
                    break;
                }
            }

            await EditProfileDialog.ShowAsync();
        }

        // 2. Збереження змінених даних
        private void EditProfileDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            CurrentUser.UserName = EditName.Text;
            CurrentUser.Age = (int)EditAge.Value;
            CurrentUser.Weight = EditWeight.Value;
            CurrentUser.Height = EditHeight.Value;
            CurrentUser.FitnessLevel = (EditLevel.SelectedItem as ComboBoxItem)?.Content.ToString();

            // Перезаписуємо JSON
            string json = JsonSerializer.Serialize(CurrentUser, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(profilePath, json);

            // Оновлюємо заголовок панелі
            ShowMainApp();
        }

        // 3. Видалення профілю
        private void EditProfileDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (File.Exists(profilePath))
            {
                File.Delete(profilePath);
            }

            CurrentUser = null;

            // Повертаємо програму до стану "без профілю"
            MainNav.Visibility = Visibility.Collapsed;
            ShowRegistrationForm();
        }


        private string planPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "my_plan.json");

        private async void BuildPlanButton_Click(object sender, RoutedEventArgs e)
        {
            BuildPlanDialog.XamlRoot = this.Content.XamlRoot;
            await BuildPlanDialog.ShowAsync();
        }

        private async void BuildPlanDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            // 1. Отримуємо вибір користувача з ComboBox
            var selectedLocation = (TrainingType)SelectLocation.SelectedIndex; // 0 - Home, 1 - Gym
            var selectedMaxDiff = (DifficultyLevel)SelectDifficulty.SelectedIndex; // 0 - Easy, 1 - AboveAverage, 2 - Hard

            // 2. Отримуємо базу вправ
            var allExercises = ExerciseRepository.GetDefaultExercises();

            // 3. Застосовуємо розширену логіку фільтрації
            var filtered = allExercises.Where(ex => {

                // --- ЛОГІКА СКЛАДНОСТІ ---
                // Якщо вибрано Easy (0) -> проходять лише Easy (0)
                // Якщо вибрано AboveAverage (1) -> проходять Easy (0) та AboveAverage (1)
                // Якщо вибрано Hard (2) -> проходять всі (<= 2)
                bool difficultyMatches = (int)ex.Difficulty <= (int)selectedMaxDiff;

                // --- ЛОГІКА ЛОКАЦІЇ ---
                // Якщо вибрано Home (0) -> проходять тільки вправи для Home (0)
                // Якщо вибрано Gym (1) -> проходять і Home (0), і Gym (1)
                bool locationMatches = false;
                if (selectedLocation == TrainingType.Home)
                {
                    locationMatches = ex.Location == TrainingType.Home;
                }
                else if (selectedLocation == TrainingType.Gym)
                {
                    locationMatches = true; // У залі можна робити все
                }

                // --- ЛОГІКА ПРОФІЛЮ (Фізичні параметри) ---
                bool profileMatches =
                    CurrentUser.Age >= ex.AgeRange[0] && CurrentUser.Age <= ex.AgeRange[1] &&
                    CurrentUser.Weight >= ex.MinWeight && CurrentUser.Weight <= ex.MaxWeight &&
                    CurrentUser.Height >= ex.MinHeight && CurrentUser.Height <= ex.MaxHeight;

                return difficultyMatches && locationMatches && profileMatches;
            }).ToList();

            // 4. Збереження результату
            UserPlan newPlan = new UserPlan
            {
                PlanName = "Список всіх вправ для тренувань",
                Exercises = filtered
            };

            string json = JsonSerializer.Serialize(newPlan, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(planPath, json);

            // Даємо час на закриття поточного діалогу
            await Task.Delay(200);

            PlanSuccessTip.Subtitle = $"Підібрано вправ: {filtered.Count}. Тепер ви можете побачити їх у розділі Тренування.";
            PlanSuccessTip.IsOpen = true;
        }

        // Допоміжний метод для швидких повідомлень
        private async void ShowMessage(string title, string content)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = title,
                Content = content,
                CloseButtonText = "Ок",
                XamlRoot = this.Content.XamlRoot
            };
            await dialog.ShowAsync();
        }


        private void MainNav_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            // Перевіряємо, чи натиснуто на звичайний пункт меню
            if (args.SelectedItem is NavigationViewItem item)
            {
                switch (item.Tag)
                {
                    case "HomePage": // <--- ДОДАЛИ ЦЕ
                        ContentFrame.Navigate(typeof(HomePage));
                        break;
                    case "WorkoutPage":
                        // Завантажуємо створену нами сторінку у Frame
                        ContentFrame.Navigate(typeof(WorkoutPage));
                        break;

                        // Тут у майбутньому будуть інші сторінки (Статистика, Досягнення)
                }
            }
        }
    }
}

