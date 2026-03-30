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
        public int CurrentStreak { get; set; } = 0; // Поточна серія
        public int RecordStreak { get; set; } = 0;  // Рекорд серії


        // Дата створення профілю
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Додай ці властивості до класу UserProfile
        public int IntensityLevel { get; set; } = 3; // Навантаження від 1 до 5
        public List<DayOfWeek> SelectedTrainingDays { get; set; } = new List<DayOfWeek>();
        public bool IsGoalSet { get; set; } = false; // Прапорець, чи налаштував користувач ціль
        public WorkoutGoal PrimaryGoal { get; set; } = WorkoutGoal.KeepFit;
    }
}