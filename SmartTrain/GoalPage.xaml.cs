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
    // клас для карток днів
    public class CalendarDay
    {
        public DayOfWeek Day { get; set; }
        public string DayName { get; set; } = string.Empty;
        public bool IsTrainingDay { get; set; }
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
        private int requiredDays = 3; // днів по дефолту
        private int selectedDaysCount = 0;

        public GoalPage()
        {
            this.InitializeComponent();
            this.Loaded += GoalPage_Loaded;
        }

        //викликається щоразу, коли відкриваємо сторінку
        private async void GoalPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists(profilePath))
            {
                string json = File.ReadAllText(profilePath);
                currentUser = JsonSerializer.Deserialize<UserProfile>(json) ?? new UserProfile();

                if (!currentUser.IsGoalSet)
                {
                    OpenGoalSetupDialog();
                }
                else
                {
                    RenderCalendar();
                }
            }
            else
            {
                Frame.Navigate(typeof(HomePage));
            }
        }

        //натискання на дні
        private void DayToggle_Click(object sender, RoutedEventArgs e)
        {
            var toggle = sender as ToggleButton;
            if (toggle == null) return;

            if (toggle.IsChecked == true)
            {
                if (selectedDaysCount >= requiredDays)
                {
                    // ліміт днів на вибір
                    toggle.IsChecked = false;
                    DaysWarningText.Text = $"Ви вже обрали максимум для вашого рівня.";
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

            // доступ лише коли достатньо обрано днів
            SetupGoalDialog.IsPrimaryButtonEnabled = (selectedDaysCount == requiredDays);
        }

        private void SetupGoalDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            // 1. ціль
            var selectedGoalTag = (GoalComboBox.SelectedItem as ComboBoxItem)?.Tag.ToString();
            Enum.TryParse(selectedGoalTag, out WorkoutGoal chosenGoal);
            currentUser.PrimaryGoal = chosenGoal;
            // 2. навантаження
            currentUser.IntensityLevel = (int)IntensitySlider.Value;
            // 3. дні
            var selectedDays = new List<DayOfWeek>();

            // записуємо які дні були обрані в календар
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

            // оновлення профілю
            string profileJson = JsonSerializer.Serialize(currentUser, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(profilePath, profileJson);

            // ==========================================
            // СЕРЦЕ АЛГОРИТМУ ГЕНЕРАЦІЇ ПЛАНУ
            // ==========================================

            // А. фільтр бази вправ
            var allExercises = ExerciseRepository.GetDefaultExercises();
            var filteredPool = allExercises.Where(ex =>
                (ex.SuitableGoals.Count == 0 || ex.SuitableGoals.Contains(currentUser.PrimaryGoal)) &&
                currentUser.Age >= ex.AgeRange[0] && currentUser.Age <= ex.AgeRange[1] &&
                currentUser.Weight >= ex.MinWeight && currentUser.Weight <= ex.MaxWeight &&
                currentUser.Height >= ex.MinHeight && currentUser.Height <= ex.MaxHeight
            ).ToList();

            // захист
            if (filteredPool.Count == 0) filteredPool = allExercises;

            // Б. Пперемішка вправ
            Random rnd = new Random();
            var shuffledPool = filteredPool.OrderBy(x => rnd.Next()).ToList();

            // В. кількість вправ від навантаження
            int exercisesPerDay = 2 + currentUser.IntensityLevel;

            // Г. вправи по дням
            WeeklyPlan newPlan = new WeeklyPlan();
            int poolIndex = 0;

            foreach (var day in currentUser.SelectedTrainingDays)
            {
                var dailyWorkout = new DailyWorkout { Day = day };

                for (int i = 0; i < exercisesPerDay; i++)
                {
                    var originalEx = shuffledPool[poolIndex % shuffledPool.Count];

                    // копія вправ
                    string exJson = JsonSerializer.Serialize(originalEx);
                    var clonedEx = JsonSerializer.Deserialize<Exercise>(exJson);

                    if (clonedEx != null)
                    {
                        // 1. математика підходів
                        clonedEx.Sets = currentUser.IntensityLevel + 1;

                        // 2. математика часу
                        int timePerOneSet = Math.Max(1, originalEx.EstimatedTimeMinutes / 3);
                        clonedEx.EstimatedTimeMinutes = timePerOneSet * clonedEx.Sets;

                        dailyWorkout.Exercises.Add(clonedEx);
                    }

                    poolIndex++;
                }
                newPlan.Workouts.Add(dailyWorkout);
            }

            // Д. розклад зберігається у файл
            string planPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "weekly_calendar.json");
            string planJson = JsonSerializer.Serialize(newPlan, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(planPath, planJson);

            // онвлення календаря
            RenderCalendar();
        }

        private WeeklyPlan currentWeeklyPlan = new WeeklyPlan();
        private string calendarPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "weekly_calendar.json");

        private void RenderCalendar()
        {
            // план з файлу
            if (File.Exists(calendarPath))
            {
                string json = File.ReadAllText(calendarPath);
                currentWeeklyPlan = JsonSerializer.Deserialize<WeeklyPlan>(json) ?? new WeeklyPlan();
            }

            var week = new List<CalendarDay>();
            var dayNames = new[] { "Нд", "Пн", "Вт", "Ср", "Чт", "Пт", "Сб" };

            // блоки днів
            for (int i = 1; i <= 7; i++)
            {
                int dayIndex = i % 7;
                DayOfWeek currentDay = (DayOfWeek)dayIndex;

                // перевірка чи є день у файлі
                bool hasTraining = currentWeeklyPlan.Workouts.Any(w => w.Day == currentDay);
                var workout = currentWeeklyPlan.Workouts.FirstOrDefault(w => w.Day == currentDay); // Знаходимо тренування

                week.Add(new CalendarDay
                {
                    Day = currentDay,
                    DayName = dayNames[dayIndex],
                    IsTrainingDay = hasTraining,
                    IsDayCompleted = workout?.IsDayCompleted ?? false, // статус
                    CompletionPercentage = workout?.CompletionPercentage ?? 0 // відсоток
                });
            }

            WeekCalendar.ItemsSource = week;
            CheckIfWeekIsCompleted();
        }

        private void WeekCalendar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedDay = WeekCalendar.SelectedItem as CalendarDay;

            if (selectedDay != null && selectedDay.IsTrainingDay)
            {
                // пошук тренування для цього дня
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


        public async void OpenGoalSetupDialog()
        {
            // РОЗУМНА ПЕРЕВІРКА РІВНЯ
            string level = currentUser.FitnessLevel?.ToLower() ?? "";
            if (level.Contains("серед") || level.Contains("intermediate"))
                requiredDays = 4;
            else if (level.Contains("проф") || level.Contains("pro") || level.Contains("просунутий"))
                requiredDays = 5;
            else
                requiredDays = 3;

            DaysRequirementText.Text = $"Ваш рівень передбачає {requiredDays} дні(в) тренувань:";

            // скидання старих днів
            selectedDaysCount = 0;
            DaysWarningText.Text = "";
            SetupGoalDialog.IsPrimaryButtonEnabled = false;

            // знімання всіх галочок
            foreach (var element in DaysTogglePanel.Children)
            {
                if (element is ToggleButton tb)
                {tb.IsChecked = false;}
            }

            IntensitySlider.Value = currentUser.IntensityLevel > 0 ? currentUser.IntensityLevel : 3;
            // відкриваємо вікно
            SetupGoalDialog.XamlRoot = this.Content.XamlRoot;
            await SetupGoalDialog.ShowAsync();
        }


        // Виконано / Пропущено
        private void MarkDone_Click(object sender, RoutedEventArgs e) => UpdateExerciseStatus(sender, true);
        private void MarkFailed_Click(object sender, RoutedEventArgs e) => UpdateExerciseStatus(sender, false);

        private void UpdateExerciseStatus(object sender, bool status)
        {
            var btn = sender as Button;
            var ex = btn?.DataContext as Exercise;
            if (ex != null)
            {
                ex.IsCompleted = status;

                var temp = DayExercisesList.ItemsSource;
                DayExercisesList.ItemsSource = null;
                DayExercisesList.ItemsSource = temp;

                CheckIfDayCanBeCompleted();
            }
        }

        // якщо всі вправи виконані, то кнопка завершення дня
        private void CheckIfDayCanBeCompleted()
        {
            var selectedDay = WeekCalendar.SelectedItem as CalendarDay;
            if (selectedDay == null) return;

            var workout = currentWeeklyPlan.Workouts.FirstOrDefault(w => w.Day == selectedDay.Day);
            if (workout != null && !workout.IsDayCompleted)
            {
                // перевірка вправ на статус null
                bool allMarked = workout.Exercises.All(e => e.IsCompleted != null);
                CompleteDayBtn.Visibility = allMarked ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                // виданення кнопки, коли день вже завершено
                CompleteDayBtn.Visibility = Visibility.Collapsed;
            }
        }


        // кнопка "завершити день"
        private void CompleteDayBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedDay = WeekCalendar.SelectedItem as CalendarDay;
            var workout = currentWeeklyPlan.Workouts.FirstOrDefault(w => w.Day == selectedDay?.Day);

            if (workout != null)
            {
                // відсоток успішних
                int total = workout.Exercises.Count;
                int success = workout.Exercises.Count(ex => ex.IsCompleted == true);

                workout.CompletionPercentage = (int)Math.Round((double)success / total * 100);
                workout.IsDayCompleted = true;

                // видалення кнопку
                CompleteDayBtn.Visibility = Visibility.Collapsed;

                // перевірка для вогника
                if (currentUser.LastWorkoutDate.Date != DateTime.Now.Date)
                {
                    currentUser.CurrentStreak++; // +1 день серії

                    // перевірка рекорда
                    if (currentUser.CurrentStreak > currentUser.RecordStreak)
                    {
                        currentUser.RecordStreak = currentUser.CurrentStreak;
                    }

                    currentUser.LastWorkoutDate = DateTime.Now; // запис дати

                    // оновлений профіль
                    string profileJson = JsonSerializer.Serialize(currentUser, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(profilePath, profileJson);
                }

                // ІСТОРІЯ
                SaveWeekHistory();
                // онов календар
                RenderCalendar();
                // перевірка всього тижня
                CheckIfWeekIsCompleted();
            }
        }


        private void CheckIfWeekIsCompleted()
        {
            // перевірка чи є план
            if (currentWeeklyPlan.Workouts.Count > 0)
            {
                bool isWeekDone = currentWeeklyPlan.Workouts.All(w => w.IsDayCompleted);
                NextWeekBtn.Visibility = isWeekDone ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        // збереження в папку History
        private void SaveWeekHistory()
        {
            string historyFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "History");
            if (!Directory.Exists(historyFolder)) Directory.CreateDirectory(historyFolder);

            // генерація назви
            string fileName = $"Week_{currentWeeklyPlan.WeekNumber}_{DateTime.Now:yyyyMMdd}.json";
            string filePath = Path.Combine(historyFolder, fileName);

            string json = JsonSerializer.Serialize(currentWeeklyPlan, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);

            // збрігання плану
            File.WriteAllText(calendarPath, json);
        }

        // кнопка "Наступний тиждень"
        private void NextWeekBtn_Click(object sender, RoutedEventArgs e)
        {
            // в кінець тиждень
            currentWeeklyPlan.WeekNumber++;

            // скидання всього
            foreach (var workout in currentWeeklyPlan.Workouts)
            {
                workout.IsDayCompleted = false;
                workout.CompletionPercentage = 0;
                foreach (var ex in workout.Exercises)
                {
                    ex.IsCompleted = null;
                }
            }

            SaveWeekHistory(); // новий план тижня
            NextWeekBtn.Visibility = Visibility.Collapsed;
            DayExercisesList.ItemsSource = null;
            SelectedDayTitle.Text = $"Тиждень {currentWeeklyPlan.WeekNumber} розпочато!";

            RenderCalendar();
        }

    }
}