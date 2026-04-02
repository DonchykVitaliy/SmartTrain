using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace SmartTrain
{
    // клас де збирається вся історія користувача
    public class AchievementStats
    {
        public int TotalCompletedDays { get; set; } = 0;
        public int TotalMinutes { get; set; } = 0;
        public int TotalSets { get; set; } = 0;
        public int CompletedWeeks { get; set; } = 0;
        public int GoldenWeeks { get; set; } = 0; // тижні на 100%
    }

    public class Achievement
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Func<UserProfile, WeeklyPlan, AchievementStats, bool> CheckCondition { get; set; } = (u, w, s) => false;
    }

    public static class AchievementManager
    {
        // БАЗА З 25 ДОСЯГНЕНЬ
        public static List<Achievement> AllAchievements = new List<Achievement>
        {
            // --- КАТЕГОРІЯ: СТАРТ ТА ОСНОВИ ---
            new Achievement { Id = "FirstProfile", Title = "Ласкаво просимо", Description = "Ви успішно створили свій перший профіль у SmartTrain.",
                CheckCondition = (u, p, s) => !string.IsNullOrEmpty(u.UserName) },

            new Achievement { Id = "FirstBlood", Title = "Перша кров", Description = "Ви відмітили свою першу вправу як виконану.",
                CheckCondition = (u, p, s) => p != null && p.Workouts.Any(w => w.Exercises.Any(e => e.IsCompleted == true)) },

            new Achievement { Id = "FirstDay", Title = "Шлях розпочато", Description = "Ви повністю завершили свій перший тренувальний день.",
                CheckCondition = (u, p, s) => s.TotalCompletedDays >= 1 },

            new Achievement { Id = "FirstWeek", Title = "Перша віха", Description = "Ви завершили повний тиждень тренувань.",
                CheckCondition = (u, p, s) => s.CompletedWeeks >= 1 },

            new Achievement { Id = "Customizer", Title = "Майстер адаптації", Description = "Ви змінили рівень навантаження під себе.",
                CheckCondition = (u, p, s) => u.IntensityLevel != 3 },

            // --- КАТЕГОРІЯ: ДИСЦИПЛІНА ТА РЕГУЛЯРНІСТЬ ---
            new Achievement { Id = "PerfectWeek", Title = "Тиждень без прогулів", Description = "Ви завершили тиждень із результатом 100% у кожному дні.",
                CheckCondition = (u, p, s) => s.GoldenWeeks >= 1 },

            new Achievement { Id = "IronMonth", Title = "Залізний місяць", Description = "Ви сумарно завершили 4 тренувальні тижні.",
                CheckCondition = (u, p, s) => s.CompletedWeeks >= 4 },

            new Achievement { Id = "Habit", Title = "Сила звички", Description = "Ви закрили 21 тренувальний день сумарно.",
                CheckCondition = (u, p, s) => s.TotalCompletedDays >= 21 },

            new Achievement { Id = "Jubilee", Title = "Ювілей", Description = "Ви завершили 50 тренувань за весь час.",
                CheckCondition = (u, p, s) => s.TotalCompletedDays >= 50 },

            new Achievement { Id = "Centurion", Title = "Центуріон", Description = "Ви досягли позначки у 100 завершених тренувань.",
                CheckCondition = (u, p, s) => s.TotalCompletedDays >= 100 },

            new Achievement { Id = "EarlyBird", Title = "Рання пташка", Description = "Ви завершили тренування до 9:00 ранку.",
                CheckCondition = (u, p, s) => DateTime.Now.Hour < 9 && p.Workouts.Any(w => w.IsDayCompleted) },

            new Achievement { Id = "NightWolf", Title = "Нічний вовк", Description = "Ви завершили тренування після 21:00 вечора.",
                CheckCondition = (u, p, s) => DateTime.Now.Hour >= 21 && p.Workouts.Any(w => w.IsDayCompleted) },

            // --- КАТЕГОРІЯ: ЯКІСТЬ ТА ІНТЕНСИВНІСТЬ ---
            new Achievement { Id = "Perfectionist", Title = "Перфекціоніст", Description = "Ви виконали тренувальний день на всі 100%.",
                CheckCondition = (u, p, s) => p.Workouts.Any(w => w.IsDayCompleted && w.CompletionPercentage == 100) },

            new Achievement { Id = "PushLimits", Title = "Вихід за межі", Description = "Ви встановили рівень навантаження 4 або 5.",
                CheckCondition = (u, p, s) => u.IntensityLevel >= 4 },

            new Achievement { Id = "BeastMode", Title = "Режим Звіра", Description = "Ви виконали тренування на 5 рівні інтенсивності з результатом 100%.",
                CheckCondition = (u, p, s) => u.IntensityLevel == 5 && p.Workouts.Any(w => w.IsDayCompleted && w.CompletionPercentage == 100) },

            new Achievement { Id = "FullRoster", Title = "Повний склад", Description = "Ви обрали 5 тренувальних днів на тиждень.",
                CheckCondition = (u, p, s) => u.SelectedTrainingDays.Count >= 5 },

            // --- КАТЕГОРІЯ: ОБ'ЄМ ТА ЧАС ---
            new Achievement { Id = "FirstHour", Title = "Перша година", Description = "Ви сумарно провели 60 хвилин за вправами.",
                CheckCondition = (u, p, s) => s.TotalMinutes >= 60 },

            new Achievement { Id = "Marathoner", Title = "Марафонець", Description = "Ви сумарно набрали 10 годин чистого часу тренувань (600 хв).",
                CheckCondition = (u, p, s) => s.TotalMinutes >= 600 },

            new Achievement { Id = "Dedication", Title = "Відданість справі", Description = "Ви провели за тренуваннями більше 24 годин.",
                CheckCondition = (u, p, s) => s.TotalMinutes >= 1440 },

            new Achievement { Id = "HalfThousand", Title = "Не зупиняйся", Description = "Ви виконали сумарно 500 підходів.",
                CheckCondition = (u, p, s) => s.TotalSets >= 500 },

            new Achievement { Id = "ThousandSets", Title = "Тисячник", Description = "Ви виконали сумарно 1000 підходів.",
                CheckCondition = (u, p, s) => s.TotalSets >= 1000 },

            // --- КАТЕГОРІЯ: ЕЛІТНІ ДОСЯГНЕННЯ ---
            new Achievement { Id = "GoldenStandard", Title = "Золотий стандарт", Description = "Ви завершили 5 «Золотих тижнів» (100% виконання).",
                CheckCondition = (u, p, s) => s.GoldenWeeks >= 5 },

            new Achievement { Id = "Equator", Title = "Екватор", Description = "Ви завершили 75 тренувальних днів.",
                CheckCondition = (u, p, s) => s.TotalCompletedDays >= 75 },

            new Achievement { Id = "TrueAthlete", Title = "Справжній атлет", Description = "Ви завершили 10 «Золотих тижнів».",
                CheckCondition = (u, p, s) => s.GoldenWeeks >= 10 },

            new Achievement { Id = "Legend", Title = "Легенда SmartTrain", Description = "Ви завершили 150 тренувальних днів.",
                CheckCondition = (u, p, s) => s.TotalCompletedDays >= 150 }
        };

        public static event Action<Achievement>? OnAchievementUnlocked;

        public static void CheckAchievements(UserProfile currentUser, WeeklyPlan currentPlan)
        {
            if (currentUser == null) return;

            // статистикв з історії користувача
            AchievementStats stats = CalculateHistoricalStats();

            bool profileChanged = false;

            // перевірка кожного досягнення
            foreach (var ach in AllAchievements)
            {
                // досягнення ще НЕ розблоковано і умова виконана
                if (!currentUser.UnlockedAchievements.Contains(ach.Id) && ach.CheckCondition(currentUser, currentPlan, stats))
                {
                    currentUser.UnlockedAchievements.Add(ach.Id);
                    profileChanged = true;

                    // віконце значка
                    OnAchievementUnlocked?.Invoke(ach);
                }
            }

            // зберігаємо у профіль
            if (profileChanged)
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "user_profile.json");
                string json = JsonSerializer.Serialize(currentUser, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(path, json);
            }
        }

        // скан папки History і підразунок успіхів
        private static AchievementStats CalculateHistoricalStats()
        {
            var stats = new AchievementStats();
            string historyFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "History");

            if (!Directory.Exists(historyFolder)) return stats;

            var files = Directory.GetFiles(historyFolder, "*.json");
            foreach (var file in files)
            {
                try
                {
                    string json = File.ReadAllText(file);
                    var weekPlan = JsonSerializer.Deserialize<WeeklyPlan>(json);

                    if (weekPlan != null)
                    {
                        bool isGoldenWeek = true;
                        bool hasCompletedDays = false;

                        foreach (var workout in weekPlan.Workouts)
                        {
                            if (workout.IsDayCompleted)
                            {
                                hasCompletedDays = true;
                                stats.TotalCompletedDays++;
                                stats.TotalMinutes += workout.TotalTimeMinutes;

                                foreach (var ex in workout.Exercises)
                                {
                                    if (ex.IsCompleted == true) stats.TotalSets += ex.Sets;
                                }

                                if (workout.CompletionPercentage < 100) isGoldenWeek = false;
                            }
                            else if (workout.Exercises.Count > 0)
                            {
                                isGoldenWeek = false; // був день з вправами, але він не завершений
                            }
                        }

                        if (hasCompletedDays && weekPlan.Workouts.Where(w => w.Exercises.Count > 0).All(w => w.IsDayCompleted))
                        {
                            stats.CompletedWeeks++;
                            if (isGoldenWeek) stats.GoldenWeeks++;
                        }
                    }
                }
                catch { /* ігнор поганих файлів історії*/ }
            }

            return stats;
        }
    }
}