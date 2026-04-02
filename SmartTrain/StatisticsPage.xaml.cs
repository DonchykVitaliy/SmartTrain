using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace SmartTrain
{
    public sealed partial class StatisticsPage : Page
    {
        private string profilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "user_profile.json");
        private UserProfile currentUser = new UserProfile();

        public StatisticsPage()
        {
            this.InitializeComponent();
            this.Loaded += StatisticsPage_Loaded;
        }

        private void StatisticsPage_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            if (File.Exists(profilePath))
            {
                string json = File.ReadAllText(profilePath);
                currentUser = JsonSerializer.Deserialize<UserProfile>(json) ?? new UserProfile();

                // шапка
                PageTitleText.Text = $"Статистика користувача {currentUser.UserName}";
                WeightInput.Text = currentUser.Weight.ToString();
                HeightText.Text = $"Зріст: {currentUser.Height} см";

                CalculateBMI();

                GoalText.Text = $"Ціль: {GetGoalName(currentUser.PrimaryGoal)}";
                IntensityText.Text = $"Рівень навантаження: {currentUser.IntensityLevel} / 5";

                // дні в рядки
                if (currentUser.SelectedTrainingDays.Count > 0)
                {
                    var ukrDays = currentUser.SelectedTrainingDays.Select(d => GetUkrDayName(d));
                    TrainingDaysText.Text = string.Join(", ", ukrDays);
                }

                CurrentStreakText.Text = $"{currentUser.CurrentStreak} дн.";
                RecordStreakText.Text = $"{currentUser.RecordStreak} дн.";

                // рахуємо глобальну статистику з History
                CalculateHistoricalStats();
            }
        }

        private void CalculateBMI()
        {
            if (currentUser.Height > 0 && currentUser.Weight > 0)
            {
                // формула: вага (кг) / (зріст (м) * ззріст (м))
                double heightM = currentUser.Height / 100.0;
                double bmi = currentUser.Weight / (heightM * heightM);

                BmiText.Text = Math.Round(bmi, 1).ToString();

                if (bmi < 18.5) { BmiStatusText.Text = "(Дефіцит маси)"; BmiStatusText.Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(Windows.UI.Color.FromArgb(255, 200, 200, 0)); }
                else if (bmi < 25) { BmiStatusText.Text = "(Норма)"; BmiStatusText.Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(Windows.UI.Color.FromArgb(255, 144, 238, 144)); }
                else if (bmi < 30) { BmiStatusText.Text = "(Зайва вага)"; BmiStatusText.Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 165, 0)); }
                else { BmiStatusText.Text = "(Ожиріння)"; BmiStatusText.Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 69, 0)); }
            }
        }

        // діаграма успішності
        private void CalculateHistoricalStats()
        {
            string historyFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "History");
            int totalMinutes = 0;
            int totalDays = 0;
            int totalSets = 0;

            int sumPercentages = 0;
            int completedWorkoutsCount = 0;

            int allExercisesEver = 0;
            int successfulExercisesEver = 0;

            if (Directory.Exists(historyFolder))
            {var files = Directory.GetFiles(historyFolder, "*.json");
                foreach (var file in files)
                {
                    try
                    {string json = File.ReadAllText(file);
                        var weekPlan = JsonSerializer.Deserialize<WeeklyPlan>(json);

                        if (weekPlan != null)
                        {
                            foreach (var workout in weekPlan.Workouts)
                            {
                                // підрахунок вправ
                                foreach (var ex in workout.Exercises)
                                {
                                    if (ex.IsCompleted != null) allExercisesEver++;
                                    if (ex.IsCompleted == true) successfulExercisesEver++;
                                }

                                if (workout.IsDayCompleted)
                                {
                                    totalDays++;
                                    totalMinutes += workout.TotalTimeMinutes;
                                    sumPercentages += workout.CompletionPercentage;
                                    completedWorkoutsCount++;

                                    foreach (var ex in workout.Exercises)
                                    {
                                        if (ex.IsCompleted == true) totalSets += ex.Sets;
                                    }
                                }
                            }
                        }
                    }
                    catch { /* іігноруємо биті файли */ }
                }}

            // --- ОНОВЛЕННЯ ТЕКСТОВОЇ СТАТИСТИКИ ---
            TotalTimeText.Text = totalMinutes >= 60 ? $"{totalMinutes / 60} год {totalMinutes % 60} хв" : $"{totalMinutes} хв";
            TotalDaysText.Text = totalDays.ToString();
            TotalSetsText.Text = totalSets.ToString();

            if (completedWorkoutsCount > 0)
            {
                int avgQuality = sumPercentages / completedWorkoutsCount;
                AverageQualityText.Text = $"{avgQuality}%";
            }

            // --- МАЛЮЄМО ДІАГРАМУ (Ring Chart) ---
            if (allExercisesEver > 0)
            {
                double successRate = ((double)successfulExercisesEver / allExercisesEver) * 100;
                SuccessRing.Value = successRate;
                SuccessRingText.Text = $"{Math.Round(successRate)}%";
            }

            // --- МАЛЮЄМО ГРАФІК АКТИВНОСТІ ---
            DrawActivityChart();
        }

        // графік поточного тижня
        private void DrawActivityChart()
        {
            string calendarPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "weekly_calendar.json");
            var chartData = new List<ChartItem>();
            int maxMinutes = 1; //захст від ділення на нуль

            if (File.Exists(calendarPath))
            {
                try
                {
                    string json = File.ReadAllText(calendarPath);
                    var currentPlan = JsonSerializer.Deserialize<WeeklyPlan>(json);

                    if (currentPlan != null)
                    {
                        // знаходимо найдовше тренування для масштабування
                        foreach (var workout in currentPlan.Workouts)
                        {
                            if (workout.TotalTimeMinutes > maxMinutes) maxMinutes = workout.TotalTimeMinutes;
                        }

                        // будуємо стовпчики
                        foreach (var workout in currentPlan.Workouts)
                        {
                            int val = workout.IsDayCompleted ? workout.TotalTimeMinutes : 0;
                            chartData.Add(new ChartItem
                            {
                                Label = GetUkrDayName(workout.Day),
                                Value = val,
                                // якщо хвилин 0 - висота 5 пікселів, інакше рахуємо пропорцію від 120 пікселів
                                BarHeight = val == 0 ? 5 : ((double)val / maxMinutes) * 120
                            });
                        }
                    }
                }
                catch { }
            }

            // якщо план порожній
            if (chartData.Count == 0)
            {
                chartData.Add(new ChartItem { Label = "Немає", Value = 0, BarHeight = 5 });
            }

            ActivityChart.ItemsSource = chartData;
        }

        private void UpdateWeight_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(WeightInput.Text, out double newWeight) && newWeight > 20 && newWeight < 300)
            {
                currentUser.Weight = newWeight;

                // профіль збр
                string json = JsonSerializer.Serialize(currentUser, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(profilePath, json);

                // оновлення ІМТ на екрані
                CalculateBMI();
            }
            else
            {
                // перевірка на текст в полі
                WeightInput.Text = currentUser.Weight.ToString();
            }
        }

        // просто гарний текст
        private string GetGoalName(WorkoutGoal goal)
        {
            switch (goal)
            {
                case WorkoutGoal.WeightLoss: return "Схуднення та рельєф";
                case WorkoutGoal.MuscleGain: return "Набір м'язової маси";
                case WorkoutGoal.KeepFit: return "Підтримка форми";
                case WorkoutGoal.Strength: return "Розвиток сили";
                case WorkoutGoal.HealthAndMobility: return "Здоров'я та гнучкість";
                default: return "Невідомо";
            }
        }

        private string GetUkrDayName(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Monday: return "Пн";
                case DayOfWeek.Tuesday: return "Вт";
                case DayOfWeek.Wednesday: return "Ср";
                case DayOfWeek.Thursday: return "Чт";
                case DayOfWeek.Friday: return "Пт";
                case DayOfWeek.Saturday: return "Сб";
                case DayOfWeek.Sunday: return "Нд";
                default: return "";
            }
        }
    }

    public class ChartItem
    {
        public string Label { get; set; } = string.Empty;
        public int Value { get; set; }
        // щоб стовпчик не був нескінченно довгим, ми масштабуємо його (макс 120 пікселів у висоту)
        public double BarHeight { get; set; }
    }
}