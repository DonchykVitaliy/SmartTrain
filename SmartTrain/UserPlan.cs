using System;
using System.Collections.Generic;

namespace SmartTrain
{
    public class UserPlan
    {
        public string PlanName { get; set; } = string.Empty;
        public List<Exercise> Exercises { get; set; } = new List<Exercise>();
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}