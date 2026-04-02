using System.Collections.Generic;

namespace SmartTrain
{

    public enum WorkoutGoal
    {
        WeightLoss,         // Схуднення та рельєф
        MuscleGain,         // Набір м'язової маси
        KeepFit,            // Підтримка форми
        Strength,           // Абсолютна сила
        HealthAndMobility   // Здоров'я
    }

    // налаштування
    public enum DifficultyLevel { Easy, AboveAverage, Hard }
    public enum TrainingType { Home, Gym }
    public enum FitnessLevelRequired { Beginner, Intermediate, Pro }
    

    public sealed class Exercise
    {
        //базоов інформація
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PlanName { get; set; } = string.Empty;

        //фільтри
        public DifficultyLevel Difficulty { get; set; }
        public TrainingType Location { get; set; }
        public int EstimatedTimeMinutes { get; set; }

        //теги + обмеження
        public List<int> AgeRange { get; set; } = new List<int>();
        public FitnessLevelRequired RequiredLevel { get; set; }
        public double MinWeight { get; set; }
        public double MaxWeight { get; set; }
        public double MinHeight { get; set; }
        public double MaxHeight { get; set; }
        public int Sets { get; set; } = 3; // базова кількість підходів
        public List<WorkoutGoal> SuitableGoals { get; set; } = new List<WorkoutGoal>();

        // для роботи JSON-серіалізатора
        public Exercise() { }

        // оновлений конструктор для створення об'єктів вручну
        public Exercise(string name, string description, DifficultyLevel difficulty, TrainingType location, int time)
        {
            Name = name;
            Description = description;
            Difficulty = difficulty;
            Location = location;
            EstimatedTimeMinutes = time;
        }

        // Ця властивість об'єднує всі теги в один текст для зручного виводу при наведенні.
        // [JsonIgnore] потрібен, щоб програма не намагалася зберігати цей текст у файл.
        [System.Text.Json.Serialization.JsonIgnore]
public string DifficultyTranslated => Difficulty switch
{
    DifficultyLevel.Easy => "Легко",
    DifficultyLevel.AboveAverage => "Вище середнього",
    DifficultyLevel.Hard => "Важко",
    _ => "Невідомо"
};

[System.Text.Json.Serialization.JsonIgnore]
public string LocationTranslated => Location switch
{
    TrainingType.Home => "Вдома",
    TrainingType.Gym => "Спортзал",
    _ => "Будь-де"
};

[System.Text.Json.Serialization.JsonIgnore]
public string LevelTranslated => RequiredLevel switch
{
    FitnessLevelRequired.Beginner => "Початківець",
    FitnessLevelRequired.Intermediate => "Середній",
    FitnessLevelRequired.Pro => "Профі",
    _ => "Будь-який"
};
        [System.Text.Json.Serialization.JsonIgnore]
        public string TooltipInfo =>
            $"Вікова категорія: від {AgeRange[0]} до {AgeRange[1]} років\n" +
            $"Рекомендована вага: {MinWeight} - {MaxWeight} кг\n" +
            $"Рекомендований зріст: {MinHeight} - {MaxHeight} см\n" +
            $"Мінімальний рівень: {LevelTranslated}";


        // null - не чіпали, true - виконано (галочка), false - пропущено (хрестик)
        public bool? IsCompleted { get; set; } = null;

        // колір фону залежно від статусу
        [System.Text.Json.Serialization.JsonIgnore]
        public object CardBackground
        {
            get
            {
                if (IsCompleted == true) return new Microsoft.UI.Xaml.Media.SolidColorBrush(Windows.UI.Color.FromArgb(40, 76, 175, 80)); // зелений
                if (IsCompleted == false) return new Microsoft.UI.Xaml.Media.SolidColorBrush(Windows.UI.Color.FromArgb(40, 244, 67, 54)); // червоний
                // стандартний колір картки
                return Microsoft.UI.Xaml.Application.Current.Resources["CardBackgroundFillColorDefaultBrush"];
            }
        }
    }
}

