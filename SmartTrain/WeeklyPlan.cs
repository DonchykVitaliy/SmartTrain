using System;
using System.Collections.Generic;

namespace SmartTrain
{
    public class DailyWorkout
    {
        public DayOfWeek Day { get; set; }
        public List<Exercise> Exercises { get; set; } = new List<Exercise>();
        public bool IsDayCompleted { get; set; } = false;
        public int CompletionPercentage { get; set; } = 0;

        [System.Text.Json.Serialization.JsonIgnore]
        public int TotalTimeMinutes
        {
            get { int total = 0; foreach (var ex in Exercises) total += ex.EstimatedTimeMinutes; return total; }
        }
    }

    public class WeeklyPlan
    {
        public int WeekNumber { get; set; } = 1; // номер тижня
        public List<DailyWorkout> Workouts { get; set; } = new List<DailyWorkout>();
        public DateTime GeneratedAt { get; set; } = DateTime.Now;
    }
}