using System;

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
    }
}