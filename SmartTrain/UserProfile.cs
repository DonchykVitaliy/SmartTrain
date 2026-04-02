using System;
using System.Collections.Generic;

namespace SmartTrain
{
    public class UserProfile
    {
        public string UserName { get; set; } = string.Empty;
        public int Age { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public string FitnessLevel { get; set; } = "Початківець";
        public int CurrentStreak { get; set; } = 0; // поточна серія
        public int RecordStreak { get; set; } = 0;  // рекорд серії
        public DateTime LastWorkoutDate { get; set; } = DateTime.MinValue;


        public List<string> UnlockedAchievements { get; set; } = new List<string>();

        // дата створення профілю
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int IntensityLevel { get; set; } = 3; // навантаження від 1 до 5
        public List<DayOfWeek> SelectedTrainingDays { get; set; } = new List<DayOfWeek>();
        public bool IsGoalSet { get; set; } = false; // прапорець, чи налаштував користувач ціль
        public WorkoutGoal PrimaryGoal { get; set; } = WorkoutGoal.KeepFit;
    }
}