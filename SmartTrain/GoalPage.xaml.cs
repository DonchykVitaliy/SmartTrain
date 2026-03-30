using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace SmartTrain
{
    // Допоміжний клас для малювання карток днів
    public class CalendarDay
    {
        public DayOfWeek Day { get; set; }
        public string DayName { get; set; } = string.Empty;
        public bool IsTrainingDay { get; set; }

        // НОВІ ПОЛЯ
        public bool IsDayCompleted { get; set; }
        public int CompletionPercentage { get; set; }

        public string Status => IsDayCompleted ? $"Виконано: {CompletionPercentage}%" : (IsTrainingDay ? "Тренування" : "Відпочинок");
        public Visibility CheckmarkVisibility => IsDayCompleted ? Visibility.Visible : Visibility.Collapsed;

        public Brush BackgroundColor => IsTrainingDay ?
            new SolidColorBrush(Windows.UI.Color.FromArgb(255, 60, 60, 60)) :
            new SolidColorBrush(Windows.UI.Color.FromArgb(0, 0, 0, 0));
    }

    public sealed partial class GoalPage : Page
    {
        private UserProfile currentUser = default!;
        private string profilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "user_profile.json");
        private int requiredDays = 3; // Кількість днів за замовчуванням
        private int selectedDaysCount = 0;

        public GoalPage()
        {
            this.InitializeComponent();
            this.Loaded += GoalPage_Loaded;
        }

        // Цей метод викликається щоразу, коли ми відкриваємо сторінку
        // Цей метод викличеться, коли сторінка вже 100% буде на екрані
        private async void GoalPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists(profilePath))
            {
                string json = File.ReadAllText(profilePath);
                currentUser = JsonSerializer.Deserialize<UserProfile>(json) ?? new UserProfile();

                if (!currentUser.IsGoalSet)
                {
                    // Встановлюємо ліміт днів залежно від досвіду
                    if (currentUser.FitnessLevel == "Початківець") requiredDays = 3;
                    else if (currentUser.FitnessLevel == "Середній") requiredDays = 4;
                    else requiredDays = 5;

                    DaysRequirementText.Text = $"Ваш рівень ({currentUser.FitnessLevel}) передбачає {requiredDays} дні(в) тренувань:";

                    // Тепер XamlRoot гарантовано існує!
                    SetupGoalDialog.XamlRoot = this.Content.XamlRoot;
                    await SetupGoalDialog.ShowAsync();
                }
                else
                {
                    RenderCalendar();
                }
            }
            else
            {
                // Якщо профілю немає - повертаємо на головну
                Frame.Navigate(typeof(HomePage));
            }

        }

        // Обробка натискання на кнопки днів (Пн, Вт...)
        private void DayToggle_Click(object sender, RoutedEventArgs e)
        {
            var toggle = sender as ToggleButton;
            if (toggle == null) return;

            if (toggle.IsChecked == true)
            {
                if (selectedDaysCount >= requiredDays)
                {
                    // Якщо ліміт вичерпано, не даємо натиснути
                    toggle.IsChecked = false;
                    DaysWarningText.Text = $"Ви вже обрали максимум ({requiredDays} дні) для вашого рівня.";
                }
                else
                {
                    selectedDaysCount++;
                    DaysWarningText.Text = "";
                }
            }
            else
            {
                selectedDaysCount--;
                DaysWarningText.Text = "";
            }

            // Кнопка генерації доступна лише якщо обрано рівно потрібну кількість днів
            SetupGoalDialog.IsPrimaryButtonEnabled = (selectedDaysCount == requiredDays);
        }

        private void SetupGoalDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            // 1. Отримуємо та зберігаємо Ціль з ComboBox
            var selectedGoalTag = (GoalComboBox.SelectedItem as ComboBoxItem)?.Tag.ToString();
            Enum.TryParse(selectedGoalTag, out WorkoutGoal chosenGoal);
            currentUser.PrimaryGoal = chosenGoal;

            // 2. Отримуємо Навантаження
            currentUser.IntensityLevel = (int)IntensitySlider.Value;

            // 3. Збираємо вибрані дні (шукаємо всі натиснуті ToggleButton)
            // 3. Збираємо вибрані дні (шукаємо всі натиснуті ToggleButton)
            var selectedDays = new List<DayOfWeek>();

            // Тепер ми шукаємо кнопки у правильній панелі!
            foreach (var element in DaysTogglePanel.Children)
            {
                if (element is ToggleButton tb && tb.IsChecked == true && tb.Tag != null)
                {
                    if (Enum.TryParse(tb.Tag.ToString(), out DayOfWeek day))
                    {
                        selectedDays.Add(day);
                    }
                }
            }
            currentUser.SelectedTrainingDays = selectedDays;
            currentUser.IsGoalSet = true;

            // Зберігаємо оновлений профіль
            string profileJson = JsonSerializer.Serialize(currentUser, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(profilePath, profileJson);

            // ==========================================
            // СЕРЦЕ АЛГОРИТМУ ГЕНЕРАЦІЇ ПЛАНУ
            // ==========================================

            // А. Фільтруємо базу вправ (за Ціллю, Віком, Вагою, Зростом)
            var allExercises = ExerciseRepository.GetDefaultExercises();
            var filteredPool = allExercises.Where(ex =>
                (ex.SuitableGoals.Count == 0 || ex.SuitableGoals.Contains(currentUser.PrimaryGoal)) &&
                currentUser.Age >= ex.AgeRange[0] && currentUser.Age <= ex.AgeRange[1] &&
                currentUser.Weight >= ex.MinWeight && currentUser.Weight <= ex.MaxWeight &&
                currentUser.Height >= ex.MinHeight && currentUser.Height <= ex.MaxHeight
            ).ToList();

            // Якщо раптом фільтр відсіяв забагато (захист від помилок)
            if (filteredPool.Count == 0) filteredPool = allExercises;

            // Б. Перемішуємо пул вправ (щоб тренування не були однаковими)
            Random rnd = new Random();
            var shuffledPool = filteredPool.OrderBy(x => rnd.Next()).ToList();

            // В. Визначаємо кількість вправ на день залежно від повзунка Інтенсивності (від 1 до 5)
            // Рівень 1 = 3 вправи, Рівень 3 = 5 вправ, Рівень 5 = 7 вправ
            int exercisesPerDay = 2 + currentUser.IntensityLevel;

            // Г. Розподіляємо вправи по днях
            WeeklyPlan newPlan = new WeeklyPlan();
            int poolIndex = 0;

            foreach (var day in currentUser.SelectedTrainingDays)
            {
                var dailyWorkout = new DailyWorkout { Day = day };

                for (int i = 0; i < exercisesPerDay; i++)
                {
                    var originalEx = shuffledPool[poolIndex % shuffledPool.Count];

                    // Створюємо "глибоку копію" вправи через JSON, щоб змінити їй параметри ізольовано від бази
                    string exJson = JsonSerializer.Serialize(originalEx);
                    var clonedEx = JsonSerializer.Deserialize<Exercise>(exJson);

                    if (clonedEx != null)
                    {
                        // 1. Математика підходів: (Навантаження 1 = 2 підходи, Нав. 3 = 4 підходи, Нав. 5 = 6 підходів)
                        clonedEx.Sets = currentUser.IntensityLevel + 1;

                        // 2. Математика часу: Базовий час ділимо на стандартні 3 підходи і множимо на нові підходи
                        int timePerOneSet = Math.Max(1, originalEx.EstimatedTimeMinutes / 3);
                        clonedEx.EstimatedTimeMinutes = timePerOneSet * clonedEx.Sets;

                        dailyWorkout.Exercises.Add(clonedEx);
                    }

                    poolIndex++;
                }
                newPlan.Workouts.Add(dailyWorkout);
            }

            // Д. Зберігаємо згенерований розклад у файл
            string planPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "weekly_calendar.json");
            string planJson = JsonSerializer.Serialize(newPlan, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(planPath, planJson);

            // Оновлюємо інтерфейс календаря
            RenderCalendar();
        }

        private WeeklyPlan currentWeeklyPlan = new WeeklyPlan();
        private string calendarPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "weekly_calendar.json");

        private void RenderCalendar()
        {
            // Читаємо план з файлу
            if (File.Exists(calendarPath))
            {
                string json = File.ReadAllText(calendarPath);
                currentWeeklyPlan = JsonSerializer.Deserialize<WeeklyPlan>(json) ?? new WeeklyPlan();
            }

            var week = new List<CalendarDay>();
            var dayNames = new[] { "Нд", "Пн", "Вт", "Ср", "Чт", "Пт", "Сб" };

            // Будуємо 7 квадратиків (від Понеділка до Неділі)
            for (int i = 1; i <= 7; i++)
            {
                int dayIndex = i % 7;
                DayOfWeek currentDay = (DayOfWeek)dayIndex;

                // Перевіряємо, чи є цей день у нашому збереженому плані
                bool hasTraining = currentWeeklyPlan.Workouts.Any(w => w.Day == currentDay);
                var workout = currentWeeklyPlan.Workouts.FirstOrDefault(w => w.Day == currentDay); // Знаходимо тренування

                week.Add(new CalendarDay
                {
                    Day = currentDay,
                    DayName = dayNames[dayIndex],
                    IsTrainingDay = hasTraining,
                    IsDayCompleted = workout?.IsDayCompleted ?? false, // Передаємо статус
                    CompletionPercentage = workout?.CompletionPercentage ?? 0 // Передаємо відсоток
                });
            }

            WeekCalendar.ItemsSource = week;
        }

        private void WeekCalendar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedDay = WeekCalendar.SelectedItem as CalendarDay;

            if (selectedDay != null && selectedDay.IsTrainingDay)
            {
                // Шукаємо тренування для цього конкретного дня
                var workoutForDay = currentWeeklyPlan.Workouts.FirstOrDefault(w => w.Day == selectedDay.Day);

                if (workoutForDay != null)
                {
                    SelectedDayTitle.Text = $"Вправи на {selectedDay.DayName} (Орієнтовний час: {workoutForDay.TotalTimeMinutes} хв)";
                    DayExercisesList.ItemsSource = workoutForDay.Exercises;
                }
            }
            else if (selectedDay != null)
            {
                SelectedDayTitle.Text = $"День відпочинку ({selectedDay.DayName})";
                DayExercisesList.ItemsSource = null;
            }
        }


        // Цей метод можна викликати з MainWindow, щоб показати вікно налаштувань
        public async void OpenGoalSetupDialog()
        {
            SetupGoalDialog.XamlRoot = this.Content.XamlRoot;
            await SetupGoalDialog.ShowAsync();
        }


        // Обробка кнопок "Виконано" / "Пропущено"
        private void MarkDone_Click(object sender, RoutedEventArgs e) => UpdateExerciseStatus(sender, true);
        private void MarkFailed_Click(object sender, RoutedEventArgs e) => UpdateExerciseStatus(sender, false);

        private void UpdateExerciseStatus(object sender, bool status)
        {
            var btn = sender as Button;
            var ex = btn?.DataContext as Exercise;
            if (ex != null)
            {
                ex.IsCompleted = status;

                // Трюк для оновлення UI (щоб змінився колір фону)
                var temp = DayExercisesList.ItemsSource;
                DayExercisesList.ItemsSource = null;
                DayExercisesList.ItemsSource = temp;

                CheckIfDayCanBeCompleted();
            }
        }

        // Перевіряємо, чи всі вправи мають статус (відмічені). Якщо так - показуємо кнопку "Завершити"
        private void CheckIfDayCanBeCompleted()
        {
            var selectedDay = WeekCalendar.SelectedItem as CalendarDay;
            if (selectedDay == null) return;

            var workout = currentWeeklyPlan.Workouts.FirstOrDefault(w => w.Day == selectedDay.Day);
            if (workout != null && !workout.IsDayCompleted)
            {
                // Перевіряємо, чи НЕ залишилося вправ зі статусом null
                bool allMarked = workout.Exercises.All(e => e.IsCompleted != null);
                CompleteDayBtn.Visibility = allMarked ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        // Кнопка "Завершити день"
        private void CompleteDayBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedDay = WeekCalendar.SelectedItem as CalendarDay;
            var workout = currentWeeklyPlan.Workouts.FirstOrDefault(w => w.Day == selectedDay?.Day);

            if (workout != null)
            {
                // Рахуємо відсоток успішних
                int total = workout.Exercises.Count;
                int success = workout.Exercises.Count(ex => ex.IsCompleted == true);

                workout.CompletionPercentage = (int)Math.Round((double)success / total * 100);
                workout.IsDayCompleted = true;

                // Ховаємо кнопку
                CompleteDayBtn.Visibility = Visibility.Collapsed;

                // ЗБЕРІГАЄМО ІСТОРІЮ
                SaveWeekHistory();

                // Оновлюємо календар (щоб з'явилась галочка і відсоток)
                RenderCalendar();

                // Перевіряємо, чи весь тиждень завершено
                CheckIfWeekIsCompleted();
            }
        }

        private void CheckIfWeekIsCompleted()
        {
            // Якщо всі тренувальні дні у плані мають IsDayCompleted == true
            bool isWeekDone = currentWeeklyPlan.Workouts.All(w => w.IsDayCompleted);
            NextWeekBtn.Visibility = isWeekDone ? Visibility.Visible : Visibility.Collapsed;
        }

        // Логіка збереження в окрему папку
        private void SaveWeekHistory()
        {
            string historyFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "History");
            if (!Directory.Exists(historyFolder)) Directory.CreateDirectory(historyFolder);

            // Назва файлу: Week_1_20260330.json
            string fileName = $"Week_{currentWeeklyPlan.WeekNumber}_{DateTime.Now:yyyyMMdd}.json";
            string filePath = Path.Combine(historyFolder, fileName);

            string json = JsonSerializer.Serialize(currentWeeklyPlan, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);

            // Також зберігаємо поточний план, щоб програма його пам'ятала при перезапуску
            File.WriteAllText(calendarPath, json);
        }

        // Кнопка "Наступний тиждень"
        private void NextWeekBtn_Click(object sender, RoutedEventArgs e)
        {
            // Збільшуємо тиждень
            currentWeeklyPlan.WeekNumber++;

            // Скидаємо статуси всіх днів та вправ для нового кола
            foreach (var workout in currentWeeklyPlan.Workouts)
            {
                workout.IsDayCompleted = false;
                workout.CompletionPercentage = 0;
                foreach (var ex in workout.Exercises)
                {
                    ex.IsCompleted = null;
                }
            }

            SaveWeekHistory(); // Зберігаємо чистий план як старт нового тижня
            NextWeekBtn.Visibility = Visibility.Collapsed;
            DayExercisesList.ItemsSource = null;
            SelectedDayTitle.Text = $"Тиждень {currentWeeklyPlan.WeekNumber} розпочато!";

            RenderCalendar();
        }

    }
}