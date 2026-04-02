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
using Windows.Media.Core;
using Windows.Media.Playback;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SmartTrain
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        //таймер
        private DispatcherTimer _achievementTimer;
        //ЧЕРГИ ДОСЯГНЕНЬ
        private Queue<Achievement> _achievementQueue = new Queue<Achievement>();
        private bool _isAchievementDialogOpen = false;
        //плеєр для звуків
        private MediaPlayer _mediaPlayer = new MediaPlayer();

        private string profilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "user_profile.json");
        private string calendarPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "weekly_calendar.json");

        public UserProfile CurrentUser { get; set; } = default!;

        public MainWindow()
        {
            this.InitializeComponent();
            CheckUserProfile();

            // подія досягнень
            AchievementManager.OnAchievementUnlocked += ShowAchievementPopup;
            // НАЛАШТУВАННЯ ТАЙМЕРА
            _achievementTimer = new DispatcherTimer();
            _achievementTimer.Interval = TimeSpan.FromSeconds(3); // перевірка кожні 3 секунди
            _achievementTimer.Tick += AchievementTimer_Tick;
            _achievementTimer.Start();
        }

        private void CheckUserProfile()
        {
            if (File.Exists(profilePath))
            {
                // перевірка профілю
                string json = File.ReadAllText(profilePath);
                CurrentUser = JsonSerializer.Deserialize<UserProfile>(json);

                ShowMainApp();
            }
            else
            {
                // вікно регістрації
                MainNav.Visibility = Visibility.Collapsed;
                ShowRegistrationForm();
            }
        }

        private void ShowMainApp()
        {
            MainNav.Visibility = Visibility.Visible;
            MainNav.PaneTitle = $"Профіль: {CurrentUser.UserName}";


            // відкриття головної сторінки
            MainNav.SelectedItem = MainNav.MenuItems[0];
            ContentFrame.Navigate(typeof(HomePage));
        }

        private void SaveProfile_Click(object sender, RoutedEventArgs e)
        {
            // файл профілю
            CurrentUser = new UserProfile
            {
                UserName = RegName.Text,
                Age = (int)RegAge.Value,
                Weight = RegWeight.Value,
                Height = RegHeight.Value,
                FitnessLevel = (RegLevel.SelectedItem as ComboBoxItem)?.Content.ToString()
            };

            // формат в JSON
            string json = JsonSerializer.Serialize(CurrentUser, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(profilePath, json);

            // закриття реєстрації
            RegistrationArea.Visibility = Visibility.Collapsed;
            ShowMainApp();
        }

        private void ShowRegistrationForm()
        {
            RegistrationArea.Visibility = Visibility.Visible;
        }

        private async void ProfileItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            EditProfileDialog.XamlRoot = this.Content.XamlRoot; // тре для WinUI 3

            // поля поточними даними
            EditName.Text = CurrentUser.UserName;
            EditAge.Value = CurrentUser.Age;
            EditWeight.Value = CurrentUser.Weight;
            EditHeight.Value = CurrentUser.Height;

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

        // зміна профілю
        private void EditProfileDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            CurrentUser.UserName = EditName.Text;
            CurrentUser.Age = (int)EditAge.Value;
            CurrentUser.Weight = EditWeight.Value;
            CurrentUser.Height = EditHeight.Value;
            CurrentUser.FitnessLevel = (EditLevel.SelectedItem as ComboBoxItem)?.Content.ToString();

            // JSON
            string json = JsonSerializer.Serialize(CurrentUser, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(profilePath, json);

            // оновлення сторінки
            ShowMainApp();
        }

        // видалення профілю
        private void EditProfileDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (File.Exists(profilePath))
            {
                File.Delete(profilePath);
            }

            CurrentUser = null;

            // назад до реєстрації
            MainNav.Visibility = Visibility.Collapsed;
            ShowRegistrationForm();
        }


        private string planPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "my_plan.json");

        private async void BuildPlanButton_Click(object sender, RoutedEventArgs e)
        {
            BuildPlanDialog.XamlRoot = this.Content.XamlRoot;
            await BuildPlanDialog.ShowAsync();
        }

        //будуємо план
        private async void BuildPlanDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var selectedLocation = (TrainingType)SelectLocation.SelectedIndex; // 0 - Home, 1 - Gym
            var selectedMaxDiff = (DifficultyLevel)SelectDifficulty.SelectedIndex; // 0 - Easy, 1 - AboveAverage, 2 - Hard

            // база вправ
            var allExercises = ExerciseRepository.GetDefaultExercises();

            // фільтрація
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
                    locationMatches = true; // в залі можна робити все
                }


                bool profileMatches =
                    CurrentUser.Age >= ex.AgeRange[0] && CurrentUser.Age <= ex.AgeRange[1] &&
                    CurrentUser.Weight >= ex.MinWeight && CurrentUser.Weight <= ex.MaxWeight &&
                    CurrentUser.Height >= ex.MinHeight && CurrentUser.Height <= ex.MaxHeight;

                return difficultyMatches && locationMatches && profileMatches;
            }).ToList();

            // збереження
            UserPlan newPlan = new UserPlan
            {
                PlanName = "Список всіх вправ для тренувань",
                Exercises = filtered
            };

            string json = JsonSerializer.Serialize(newPlan, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(planPath, json);

            await Task.Delay(200);

            PlanSuccessTip.Subtitle = $"Підібрано вправ: {filtered.Count}. Тепер ви можете побачити їх у розділі Тренування.";
            PlanSuccessTip.IsOpen = true;
        }

        // метод для повідомлень
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
            // меню навігації
            if (args.SelectedItem is NavigationViewItem item)
            {
                // лише коли відкрита ЦІЛЬ
                ChangeGoalButton.Visibility = item.Tag?.ToString() == "GoalPage" ? Visibility.Visible : Visibility.Collapsed;

                switch (item.Tag)
                {
                    case "HomePage":
                        ContentFrame.Navigate(typeof(HomePage));
                        break;
                    case "WorkoutPage":
                        ContentFrame.Navigate(typeof(WorkoutPage));
                        break;

                    case "GoalPage": 
                        ContentFrame.Navigate(typeof(GoalPage)); break;
                    case "StatsPage":
                        ContentFrame.Navigate(typeof(StatisticsPage)); break;
                    case "AchievementsPage":
                        ContentFrame.Navigate(typeof(AchievementsPage)); break;
                }
            }
        }


        private void ChangeGoalButton_Click(object sender, RoutedEventArgs e)
        {
            // перевірка на GoalPage
            if (ContentFrame.Content is GoalPage goalPage)
            {
                goalPage.OpenGoalSetupDialog();
            }
        }




        private void AchievementTimer_Tick(object sender, object e)
        {
            // завантаження всіх файлів для досягнень
            if (File.Exists(profilePath))
            {
                try
                {
                    string userJson = File.ReadAllText(profilePath);
                    var user = JsonSerializer.Deserialize<UserProfile>(userJson);

                    WeeklyPlan plan = null;
                    if (File.Exists(calendarPath))
                    {
                        string planJson = File.ReadAllText(calendarPath);
                        plan = JsonSerializer.Deserialize<WeeklyPlan>(planJson);
                    }

                    // перевірка
                    if (user != null)
                    {
                        AchievementManager.CheckAchievements(user, plan);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Помилка таймера досягнень: {ex.Message}");
                }
            }
        }



        // нове досягнення
        private void ShowAchievementPopup(Achievement ach)
        {
            this.DispatcherQueue.TryEnqueue(() =>
            {
                _achievementQueue.Enqueue(ach); // ставимо у чергу
                ProcessAchievementQueue();      // конвеєр
            });
        }

        // цей метод бере досягнення з черги по одному
        private async void ProcessAchievementQueue()
        {
            // Якщо вікно ВЖЕ відкрите, або черга порожня - нічого не робимо
            if (_isAchievementDialogOpen || _achievementQueue.Count == 0)
                return;

            _isAchievementDialogOpen = true; // Блокуємо екран для наступних вікон

            // Дістаємо ПЕРШЕ досягнення з черги
            var ach = _achievementQueue.Dequeue();

            if (this.Content is FrameworkElement root && root.XamlRoot != null)
            {
                AchievementPopupDialog.XamlRoot = root.XamlRoot;
            }

            AchievementSubtitleText.Text = ach.Title;
            AchievementDescText.Text = ach.Description;

            PlayAchievementSound();

            await AchievementPopupDialog.ShowAsync();

            _isAchievementDialogOpen = false; // знімає блок
            ProcessAchievementQueue(); // викликаємо себе ж для черги
        }

        // кнопка "Отримати"
        private void AchievementPopupDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        { //пусто бо ніц не треба))
        }

        private void PlayAchievementSound()
        {
            try
            {
                // звук в папці Assets
                var uri = new Uri("ms-appx:///Assets/achievement.mp3");

                // звук у плеєр
                _mediaPlayer.Source = MediaSource.CreateFromUri(uri);
                _mediaPlayer.Play();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Помилка відтворення звуку: {ex.Message}");
            }
        }
    }
}

