using System.Collections.Generic;

namespace SmartTrain
{
    public static class ExerciseRepository
    {
        public static List<Exercise> GetDefaultExercises()
        {
            return new List<Exercise>
            {
                // ==========================================
                // КАТЕГОРІЯ 1: ГРУДНІ М'ЯЗИ (ДІМ ТА ЗАЛ)
                // ==========================================
                new Exercise("Класичні віджимання", "Базова вправа на груди та трицепс.", DifficultyLevel.Easy, TrainingType.Home, 10)
                { AgeRange = new List<int>{10, 70}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 40, MaxWeight = 130, MinHeight = 130, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.MuscleGain, WorkoutGoal.KeepFit, WorkoutGoal.WeightLoss } },

                new Exercise("Віджимання з широкою постановкою", "Акцент на зовнішню частину грудних.", DifficultyLevel.AboveAverage, TrainingType.Home, 10)
                { AgeRange = new List<int>{12, 65}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 45, MaxWeight = 120, MinHeight = 140, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.MuscleGain, WorkoutGoal.KeepFit } },

                new Exercise("Вузькі (алмазні) віджимання", "Акцент на внутрішню частину грудей та трицепс.", DifficultyLevel.Hard, TrainingType.Home, 8)
                { AgeRange = new List<int>{14, 60}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 50, MaxWeight = 110, MinHeight = 140, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.MuscleGain, WorkoutGoal.Strength } },

                new Exercise("Віджимання з ногами на підвищенні", "Акцент на верхню частину грудних.", DifficultyLevel.AboveAverage, TrainingType.Home, 10)
                { AgeRange = new List<int>{14, 60}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 45, MaxWeight = 115, MinHeight = 140, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.MuscleGain, WorkoutGoal.KeepFit } },

                new Exercise("Віджимання з бавовною (пліометричні)", "Вибухова сила грудних м'язів.", DifficultyLevel.Hard, TrainingType.Home, 5)
                { AgeRange = new List<int>{16, 45}, RequiredLevel = FitnessLevelRequired.Pro, MinWeight = 50, MaxWeight = 95, MinHeight = 150, MaxHeight = 200, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.Strength, WorkoutGoal.WeightLoss } },

                new Exercise("Жим штанги лежачи", "Головна базова вправа для грудей.", DifficultyLevel.AboveAverage, TrainingType.Gym, 15)
                { AgeRange = new List<int>{16, 65}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 50, MaxWeight = 150, MinHeight = 150, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.MuscleGain, WorkoutGoal.Strength } },

                new Exercise("Жим гантелей лежачи", "Дозволяє збільшити амплітуду руху.", DifficultyLevel.AboveAverage, TrainingType.Gym, 12)
                { AgeRange = new List<int>{16, 65}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 50, MaxWeight = 150, MinHeight = 140, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.MuscleGain, WorkoutGoal.KeepFit } },

                new Exercise("Жим штанги на похилій лаві (вгору)", "Акцент на верхні грудні м'язи.", DifficultyLevel.AboveAverage, TrainingType.Gym, 12)
                { AgeRange = new List<int>{16, 60}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 55, MaxWeight = 140, MinHeight = 150, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.MuscleGain, WorkoutGoal.Strength } },

                new Exercise("Жим штанги на похилій лаві (вниз)", "Акцент на нижні грудні м'язи.", DifficultyLevel.Hard, TrainingType.Gym, 12)
                { AgeRange = new List<int>{18, 55}, RequiredLevel = FitnessLevelRequired.Pro, MinWeight = 60, MaxWeight = 130, MinHeight = 150, MaxHeight = 200, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.MuscleGain, WorkoutGoal.Strength } },

                new Exercise("Розведення гантелей лежачи", "Ізолююча вправа для розтягнення грудних.", DifficultyLevel.Easy, TrainingType.Gym, 10)
                { AgeRange = new List<int>{14, 70}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 45, MaxWeight = 150, MinHeight = 140, MaxHeight = 220, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.KeepFit, WorkoutGoal.HealthAndMobility } },

                new Exercise("Зведення рук у кросовері", "Формуюча вправа на блочному тренажері.", DifficultyLevel.AboveAverage, TrainingType.Gym, 10)
                { AgeRange = new List<int>{16, 65}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 50, MaxWeight = 150, MinHeight = 150, MaxHeight = 220, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.MuscleGain, WorkoutGoal.KeepFit } },

                new Exercise("Пуловер з гантеллю", "Розширення грудної клітки.", DifficultyLevel.AboveAverage, TrainingType.Gym, 10)
                { AgeRange = new List<int>{16, 60}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 50, MaxWeight = 130, MinHeight = 150, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.MuscleGain, WorkoutGoal.HealthAndMobility } },

                new Exercise("Віджимання на брусах (грудний стиль)", "Акцент на низ грудей.", DifficultyLevel.Hard, TrainingType.Gym, 10)
                { AgeRange = new List<int>{16, 55}, RequiredLevel = FitnessLevelRequired.Pro, MinWeight = 55, MaxWeight = 110, MinHeight = 150, MaxHeight = 200, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.MuscleGain, WorkoutGoal.Strength } },

                new Exercise("Зведення рук у тренажері 'Метелик'", "Безпечна ізоляція грудних.", DifficultyLevel.Easy, TrainingType.Gym, 8)
                { AgeRange = new List<int>{14, 75}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 40, MaxWeight = 160, MinHeight = 140, MaxHeight = 220, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.KeepFit, WorkoutGoal.HealthAndMobility } },

                new Exercise("Жим Свенда", "Ізометричне навантаження на центр грудей.", DifficultyLevel.Easy, TrainingType.Gym, 5)
                { AgeRange = new List<int>{14, 70}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 45, MaxWeight = 150, MinHeight = 140, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.KeepFit, WorkoutGoal.WeightLoss } },

                // ==========================================
                // КАТЕГОРІЯ 2: СПИНА (ДІМ ТА ЗАЛ)
                // ==========================================
                new Exercise("Підтягування широким хватом", "Розвиток найширших м'язів спини.", DifficultyLevel.Hard, TrainingType.Gym, 10)
                { AgeRange = new List<int>{14, 55}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 45, MaxWeight = 105, MinHeight = 150, MaxHeight = 200, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.MuscleGain, WorkoutGoal.Strength } },

                new Exercise("Підтягування зворотним хватом", "Спина та біцепс.", DifficultyLevel.AboveAverage, TrainingType.Gym, 10)
                { AgeRange = new List<int>{14, 60}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 45, MaxWeight = 110, MinHeight = 150, MaxHeight = 200, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.MuscleGain, WorkoutGoal.KeepFit } },

                new Exercise("Супермен", "Статичне зміцнення попереку.", DifficultyLevel.Easy, TrainingType.Home, 5)
                { AgeRange = new List<int>{10, 75}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 40, MaxWeight = 150, MinHeight = 120, MaxHeight = 220, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.HealthAndMobility, WorkoutGoal.KeepFit } },

                new Exercise("Тяга верхнього блоку до грудей", "Альтернатива підтягуванням.", DifficultyLevel.Easy, TrainingType.Gym, 10)
                { AgeRange = new List<int>{14, 75}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 40, MaxWeight = 160, MinHeight = 140, MaxHeight = 220, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.MuscleGain, WorkoutGoal.KeepFit } },

                new Exercise("Тяга верхнього блоку за голову", "Акцент на верх спини.", DifficultyLevel.AboveAverage, TrainingType.Gym, 10)
                { AgeRange = new List<int>{16, 60}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 50, MaxWeight = 140, MinHeight = 150, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.MuscleGain } },

                new Exercise("Тяга нижнього блоку до пояса", "Розвиток товщини спини.", DifficultyLevel.Easy, TrainingType.Gym, 12)
                { AgeRange = new List<int>{14, 70}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 45, MaxWeight = 150, MinHeight = 140, MaxHeight = 220, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.MuscleGain, WorkoutGoal.KeepFit } },

                new Exercise("Тяга штанги в нахилі", "Базова вправа на товщину спини.", DifficultyLevel.Hard, TrainingType.Gym, 15)
                { AgeRange = new List<int>{18, 55}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 60, MaxWeight = 140, MinHeight = 155, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.Strength, WorkoutGoal.MuscleGain } },

                new Exercise("Тяга гантелі однією рукою в нахилі", "Ізольоване опрацювання найширших.", DifficultyLevel.AboveAverage, TrainingType.Gym, 12)
                { AgeRange = new List<int>{16, 65}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 50, MaxWeight = 150, MinHeight = 150, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.MuscleGain, WorkoutGoal.KeepFit } },

                new Exercise("Станова тяга", "Фундаментальна вправа для всього тіла.", DifficultyLevel.Hard, TrainingType.Gym, 20)
                { AgeRange = new List<int>{18, 50}, RequiredLevel = FitnessLevelRequired.Pro, MinWeight = 65, MaxWeight = 130, MinHeight = 160, MaxHeight = 200, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.Strength, WorkoutGoal.MuscleGain } },

                new Exercise("Гіперекстензія", "Зміцнення розгиначів спини.", DifficultyLevel.Easy, TrainingType.Gym, 10)
                { AgeRange = new List<int>{14, 70}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 45, MaxWeight = 140, MinHeight = 145, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.HealthAndMobility, WorkoutGoal.KeepFit } },

                new Exercise("Тяга Т-грифа", "Безпечна альтернатива тязі штанги.", DifficultyLevel.AboveAverage, TrainingType.Gym, 12)
                { AgeRange = new List<int>{16, 60}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 55, MaxWeight = 140, MinHeight = 155, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.MuscleGain, WorkoutGoal.Strength } },

                new Exercise("Шраги зі штангою", "Розвиток трапецієподібних м'язів.", DifficultyLevel.AboveAverage, TrainingType.Gym, 8)
                { AgeRange = new List<int>{16, 65}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 55, MaxWeight = 150, MinHeight = 150, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.MuscleGain } },

                new Exercise("Шраги з гантелями", "Більш природна амплітуда для трапецій.", DifficultyLevel.Easy, TrainingType.Gym, 8)
                { AgeRange = new List<int>{14, 70}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 50, MaxWeight = 150, MinHeight = 140, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.MuscleGain, WorkoutGoal.KeepFit } },

                new Exercise("Пловець (Swimmer)", "Динамічна вправа на підлозі для спини.", DifficultyLevel.Easy, TrainingType.Home, 5)
                { AgeRange = new List<int>{10, 75}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 40, MaxWeight = 150, MinHeight = 120, MaxHeight = 220, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.HealthAndMobility, WorkoutGoal.KeepFit } },

                new Exercise("Зворотні розведення в тренажері", "Акцент на задні дельти та центр спини.", DifficultyLevel.Easy, TrainingType.Gym, 10)
                { AgeRange = new List<int>{14, 70}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 45, MaxWeight = 150, MinHeight = 140, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.KeepFit, WorkoutGoal.MuscleGain } },

                // ==========================================
                // КАТЕГОРІЯ 3: НОГИ ТА СІДНИЦІ (ДІМ ТА ЗАЛ)
                // ==========================================
                new Exercise("Класичні присідання (без ваги)", "Основа для розвитку ніг.", DifficultyLevel.Easy, TrainingType.Home, 10)
                { AgeRange = new List<int>{8, 80}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 30, MaxWeight = 160, MinHeight = 120, MaxHeight = 220, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.KeepFit, WorkoutGoal.WeightLoss, WorkoutGoal.HealthAndMobility } },

                new Exercise("Присідання сумо", "Акцент на внутрішню частину стегна.", DifficultyLevel.Easy, TrainingType.Home, 10)
                { AgeRange = new List<int>{10, 75}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 35, MaxWeight = 150, MinHeight = 130, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.KeepFit, WorkoutGoal.WeightLoss } },

                new Exercise("Присідання з вистрибуванням", "Пліометрика для вибухової сили ніг.", DifficultyLevel.AboveAverage, TrainingType.Home, 8)
                { AgeRange = new List<int>{14, 50}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 45, MaxWeight = 100, MinHeight = 140, MaxHeight = 200, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.WeightLoss, WorkoutGoal.Strength } },

                new Exercise("Випади вперед", "Формування квадрицепсів та сідниць.", DifficultyLevel.AboveAverage, TrainingType.Home, 10)
                { AgeRange = new List<int>{12, 65}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 40, MaxWeight = 130, MinHeight = 140, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.KeepFit, WorkoutGoal.MuscleGain, WorkoutGoal.WeightLoss } },

                new Exercise("Зворотні випади", "Більш безпечно для колін.", DifficultyLevel.Easy, TrainingType.Home, 10)
                { AgeRange = new List<int>{12, 70}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 40, MaxWeight = 140, MinHeight = 140, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.KeepFit, WorkoutGoal.HealthAndMobility } },

                new Exercise("Бокові випади", "Розвиток привідних м'язів стегна.", DifficultyLevel.AboveAverage, TrainingType.Home, 10)
                { AgeRange = new List<int>{14, 60}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 45, MaxWeight = 120, MinHeight = 140, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.KeepFit, WorkoutGoal.MuscleGain } },

                new Exercise("Сідничний місток", "Ізольоване навантаження на сідниці.", DifficultyLevel.Easy, TrainingType.Home, 8)
                { AgeRange = new List<int>{10, 75}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 35, MaxWeight = 150, MinHeight = 130, MaxHeight = 220, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.KeepFit, WorkoutGoal.HealthAndMobility } },

                new Exercise("Підйом на носки стоячи", "Розвиток литкових м'язів.", DifficultyLevel.Easy, TrainingType.Home, 5)
                { AgeRange = new List<int>{10, 80}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 35, MaxWeight = 160, MinHeight = 130, MaxHeight = 220, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.KeepFit, WorkoutGoal.HealthAndMobility } },

                new Exercise("Присідання зі штангою на плечах", "Король базових вправ.", DifficultyLevel.Hard, TrainingType.Gym, 15)
                { AgeRange = new List<int>{16, 55}, RequiredLevel = FitnessLevelRequired.Pro, MinWeight = 55, MaxWeight = 140, MinHeight = 155, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.Strength, WorkoutGoal.MuscleGain } },

                new Exercise("Фронтальні присідання", "Акцент на квадрицепс та кор.", DifficultyLevel.Hard, TrainingType.Gym, 12)
                { AgeRange = new List<int>{16, 50}, RequiredLevel = FitnessLevelRequired.Pro, MinWeight = 60, MaxWeight = 130, MinHeight = 160, MaxHeight = 200, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.Strength, WorkoutGoal.MuscleGain } },

                new Exercise("Жим ногами в тренажері", "Безпечна база для великих ваг.", DifficultyLevel.AboveAverage, TrainingType.Gym, 12)
                { AgeRange = new List<int>{14, 65}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 50, MaxWeight = 160, MinHeight = 150, MaxHeight = 220, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.MuscleGain, WorkoutGoal.Strength } },

                new Exercise("Розгинання ніг у тренажері", "Ізоляція квадрицепса.", DifficultyLevel.Easy, TrainingType.Gym, 10)
                { AgeRange = new List<int>{14, 75}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 40, MaxWeight = 150, MinHeight = 140, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.KeepFit, WorkoutGoal.MuscleGain } },

                new Exercise("Згинання ніг у тренажері лежачи", "Ізоляція біцепса стегна.", DifficultyLevel.Easy, TrainingType.Gym, 10)
                { AgeRange = new List<int>{14, 75}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 40, MaxWeight = 150, MinHeight = 140, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.KeepFit, WorkoutGoal.MuscleGain } },

                new Exercise("Румунська тяга", "Ефективна розтяжка задньої поверхні стегна.", DifficultyLevel.AboveAverage, TrainingType.Gym, 12)
                { AgeRange = new List<int>{16, 60}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 55, MaxWeight = 140, MinHeight = 150, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.MuscleGain, WorkoutGoal.HealthAndMobility } },

                new Exercise("Болгарські спліт-присідання", "Асиметричне навантаження на кожну ногу.", DifficultyLevel.Hard, TrainingType.Gym, 10)
                { AgeRange = new List<int>{16, 55}, RequiredLevel = FitnessLevelRequired.Pro, MinWeight = 50, MaxWeight = 120, MinHeight = 150, MaxHeight = 200, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.MuscleGain, WorkoutGoal.WeightLoss } },

                new Exercise("Гакк-присідання", "Зняття навантаження з попереку.", DifficultyLevel.AboveAverage, TrainingType.Gym, 12)
                { AgeRange = new List<int>{16, 60}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 55, MaxWeight = 150, MinHeight = 150, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.MuscleGain, WorkoutGoal.Strength } },

                // ==========================================
                // КАТЕГОРІЯ 4: ПЛЕСЧІ ТА РУКИ (ДІМ ТА ЗАЛ)
                // ==========================================
                new Exercise("Армійський жим (стоячи)", "Жим штанги над головою.", DifficultyLevel.Hard, TrainingType.Gym, 12)
                { AgeRange = new List<int>{18, 55}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 60, MaxWeight = 130, MinHeight = 160, MaxHeight = 200, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.Strength, WorkoutGoal.MuscleGain } },

                new Exercise("Жим гантелей сидячи", "Базова вправа на передню та середню дельту.", DifficultyLevel.AboveAverage, TrainingType.Gym, 10)
                { AgeRange = new List<int>{16, 65}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 50, MaxWeight = 140, MinHeight = 150, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.MuscleGain, WorkoutGoal.KeepFit } },

                new Exercise("Махи гантелями в сторони", "Розширення плечей (середня дельта).", DifficultyLevel.Easy, TrainingType.Gym, 8)
                { AgeRange = new List<int>{14, 70}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 45, MaxWeight = 150, MinHeight = 140, MaxHeight = 220, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.KeepFit, WorkoutGoal.MuscleGain } },

                new Exercise("Махи гантелями перед собою", "Ізоляція передньої дельти.", DifficultyLevel.Easy, TrainingType.Gym, 8)
                { AgeRange = new List<int>{14, 70}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 45, MaxWeight = 150, MinHeight = 140, MaxHeight = 220, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.KeepFit, WorkoutGoal.MuscleGain } },

                new Exercise("Махи гантелями в нахилі", "Розвиток задньої дельти.", DifficultyLevel.AboveAverage, TrainingType.Gym, 10)
                { AgeRange = new List<int>{16, 60}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 50, MaxWeight = 140, MinHeight = 150, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.KeepFit, WorkoutGoal.MuscleGain } },

                new Exercise("Тяга штанги до підборіддя", "Комплексний розвиток плечового поясу.", DifficultyLevel.AboveAverage, TrainingType.Gym, 10)
                { AgeRange = new List<int>{16, 60}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 55, MaxWeight = 140, MinHeight = 150, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.MuscleGain, WorkoutGoal.KeepFit } },

                new Exercise("Зворотні віджимання від лави", "Вправа на трицепс із власною вагою.", DifficultyLevel.Easy, TrainingType.Home, 8)
                { AgeRange = new List<int>{12, 65}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 40, MaxWeight = 120, MinHeight = 140, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.KeepFit, WorkoutGoal.WeightLoss } },

                new Exercise("Віджимання на брусах (трицепс)", "Акцент на трицепс (вертикальний корпус).", DifficultyLevel.Hard, TrainingType.Gym, 10)
                { AgeRange = new List<int>{16, 50}, RequiredLevel = FitnessLevelRequired.Pro, MinWeight = 55, MaxWeight = 105, MinHeight = 150, MaxHeight = 200, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.Strength, WorkoutGoal.MuscleGain } },

                new Exercise("Жим штанги вузьким хватом", "Базова вправа для маси трицепса.", DifficultyLevel.AboveAverage, TrainingType.Gym, 12)
                { AgeRange = new List<int>{16, 60}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 55, MaxWeight = 140, MinHeight = 150, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.Strength, WorkoutGoal.MuscleGain } },

                new Exercise("Французький жим лежачи", "Ізоляція довгої головки трицепса.", DifficultyLevel.AboveAverage, TrainingType.Gym, 10)
                { AgeRange = new List<int>{16, 60}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 50, MaxWeight = 130, MinHeight = 150, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.MuscleGain, WorkoutGoal.KeepFit } },

                new Exercise("Розгинання рук на блоці", "Формуюча вправа на трицепс.", DifficultyLevel.Easy, TrainingType.Gym, 8)
                { AgeRange = new List<int>{14, 75}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 40, MaxWeight = 150, MinHeight = 140, MaxHeight = 220, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.KeepFit, WorkoutGoal.MuscleGain } },

                new Exercise("Підйом штанги на біцепс стоячи", "Класика для об'єму біцепса.", DifficultyLevel.AboveAverage, TrainingType.Gym, 10)
                { AgeRange = new List<int>{14, 65}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 50, MaxWeight = 140, MinHeight = 145, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.MuscleGain, WorkoutGoal.Strength } },

                new Exercise("Молотки (Hammer Curls)", "Розвиток брахіаліса.", DifficultyLevel.Easy, TrainingType.Gym, 8)
                { AgeRange = new List<int>{14, 70}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 45, MaxWeight = 150, MinHeight = 140, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.KeepFit, WorkoutGoal.MuscleGain } },

                new Exercise("Концентрований підйом на біцепс", "Формування піку біцепса.", DifficultyLevel.Easy, TrainingType.Gym, 8)
                { AgeRange = new List<int>{16, 65}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 45, MaxWeight = 140, MinHeight = 140, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.KeepFit, WorkoutGoal.MuscleGain } },

                new Exercise("Підйом на біцепс у лаві Скотта", "Сувора ізоляція біцепса.", DifficultyLevel.AboveAverage, TrainingType.Gym, 10)
                { AgeRange = new List<int>{16, 60}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 50, MaxWeight = 140, MinHeight = 150, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.MuscleGain, WorkoutGoal.KeepFit } },

                new Exercise("Обертання рук (Arm Circles)", "Розминка для плечових суглобів.", DifficultyLevel.Easy, TrainingType.Home, 5)
                { AgeRange = new List<int>{8, 85}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 30, MaxWeight = 180, MinHeight = 120, MaxHeight = 230, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.HealthAndMobility, WorkoutGoal.KeepFit } },

                // ==========================================
                // КАТЕГОРІЯ 5: ПРЕС ТА КОР (ДІМ ТА ЗАЛ)
                // ==========================================
                new Exercise("Планка на ліктях", "Статичне зміцнення кору.", DifficultyLevel.Easy, TrainingType.Home, 5)
                { AgeRange = new List<int>{10, 70}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 40, MaxWeight = 130, MinHeight = 130, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.HealthAndMobility, WorkoutGoal.KeepFit, WorkoutGoal.WeightLoss } },

                new Exercise("Бокова планка", "Зміцнення косих м'язів живота.", DifficultyLevel.AboveAverage, TrainingType.Home, 5)
                { AgeRange = new List<int>{14, 60}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 45, MaxWeight = 110, MinHeight = 140, MaxHeight = 200, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.HealthAndMobility, WorkoutGoal.KeepFit } },

                new Exercise("Класичні скручування", "Опрацювання верхньої частини преса.", DifficultyLevel.Easy, TrainingType.Home, 8)
                { AgeRange = new List<int>{10, 75}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 35, MaxWeight = 140, MinHeight = 130, MaxHeight = 220, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.KeepFit, WorkoutGoal.WeightLoss } },

                new Exercise("Зворотні скручування", "Опрацювання нижньої частини преса.", DifficultyLevel.AboveAverage, TrainingType.Home, 8)
                { AgeRange = new List<int>{12, 65}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 40, MaxWeight = 130, MinHeight = 140, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.KeepFit, WorkoutGoal.WeightLoss } },

                new Exercise("Російські скручування (Russian Twists)", "Ротаційна вправа для косих м'язів.", DifficultyLevel.AboveAverage, TrainingType.Home, 7)
                { AgeRange = new List<int>{14, 60}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 45, MaxWeight = 120, MinHeight = 140, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.KeepFit, WorkoutGoal.WeightLoss } },

                new Exercise("Велосипед (лежачи)", "Комплексне навантаження на прес.", DifficultyLevel.AboveAverage, TrainingType.Home, 7)
                { AgeRange = new List<int>{12, 65}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 40, MaxWeight = 125, MinHeight = 140, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.KeepFit, WorkoutGoal.WeightLoss } },

                new Exercise("Підйом ніг у висі", "Найкраща вправа для нижнього преса.", DifficultyLevel.Hard, TrainingType.Gym, 10)
                { AgeRange = new List<int>{16, 50}, RequiredLevel = FitnessLevelRequired.Pro, MinWeight = 50, MaxWeight = 100, MinHeight = 150, MaxHeight = 200, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.Strength, WorkoutGoal.KeepFit } },

                new Exercise("Скручування на фітболі", "Більша амплітуда розтягування преса.", DifficultyLevel.Easy, TrainingType.Gym, 8)
                { AgeRange = new List<int>{14, 70}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 45, MaxWeight = 130, MinHeight = 140, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.HealthAndMobility, WorkoutGoal.KeepFit } },

                new Exercise("Дроворуб (на блоці)", "Функціональне зміцнення косих м'язів.", DifficultyLevel.AboveAverage, TrainingType.Gym, 10)
                { AgeRange = new List<int>{16, 60}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 50, MaxWeight = 140, MinHeight = 150, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.KeepFit, WorkoutGoal.MuscleGain } },

                new Exercise("Вакуум живота", "Тренування поперечного м'яза живота.", DifficultyLevel.Easy, TrainingType.Home, 5)
                { AgeRange = new List<int>{12, 70}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 40, MaxWeight = 150, MinHeight = 130, MaxHeight = 220, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.HealthAndMobility, WorkoutGoal.KeepFit } },

                new Exercise("Альпініст (Mountain Climbers)", "Динамічна планка.", DifficultyLevel.AboveAverage, TrainingType.Home, 7)
                { AgeRange = new List<int>{14, 60}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 45, MaxWeight = 110, MinHeight = 140, MaxHeight = 200, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.WeightLoss, WorkoutGoal.KeepFit } },

                new Exercise("Складка (V-Ups)", "Одночасний підйом рук і ніг.", DifficultyLevel.Hard, TrainingType.Home, 8)
                { AgeRange = new List<int>{16, 50}, RequiredLevel = FitnessLevelRequired.Pro, MinWeight = 50, MaxWeight = 95, MinHeight = 150, MaxHeight = 195, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.Strength, WorkoutGoal.KeepFit } },

                new Exercise("Дотик до п'ят лежачи", "Ізоляція бокових м'язів.", DifficultyLevel.Easy, TrainingType.Home, 5)
                { AgeRange = new List<int>{10, 75}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 40, MaxWeight = 140, MinHeight = 130, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.KeepFit, WorkoutGoal.WeightLoss } },

                new Exercise("Скручування на римському стільці", "Прес із фіксованими ногами.", DifficultyLevel.AboveAverage, TrainingType.Gym, 8)
                { AgeRange = new List<int>{14, 60}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 45, MaxWeight = 130, MinHeight = 145, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.KeepFit, WorkoutGoal.MuscleGain } },

                new Exercise("Ножиці", "Нижній прес.", DifficultyLevel.Easy, TrainingType.Home, 6)
                { AgeRange = new List<int>{10, 70}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 40, MaxWeight = 135, MinHeight = 130, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.KeepFit, WorkoutGoal.WeightLoss } },

                // ==========================================
                // КАТЕГОРІЯ 6: КАРДІО ТА ВИТРИВАЛІСТЬ (ДІМ ТА ЗАЛ)
                // ==========================================
                new Exercise("Стрибки 'Джекі' (Jumping Jacks)", "Розігрів всього тіла.", DifficultyLevel.Easy, TrainingType.Home, 10)
                { AgeRange = new List<int>{8, 70}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 30, MaxWeight = 120, MinHeight = 120, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.WeightLoss, WorkoutGoal.HealthAndMobility } },

                new Exercise("Берпі", "Універсальна вправа на витривалість.", DifficultyLevel.Hard, TrainingType.Home, 15)
                { AgeRange = new List<int>{16, 50}, RequiredLevel = FitnessLevelRequired.Pro, MinWeight = 50, MaxWeight = 100, MinHeight = 150, MaxHeight = 195, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.WeightLoss, WorkoutGoal.Strength } },

                new Exercise("Стрибки на скакалці", "Розвиток координації та дихання.", DifficultyLevel.AboveAverage, TrainingType.Home, 15)
                { AgeRange = new List<int>{10, 60}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 40, MaxWeight = 110, MinHeight = 130, MaxHeight = 200, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.WeightLoss, WorkoutGoal.KeepFit } },

                new Exercise("Біг на місці з високим підняттям колін", "Інтенсивне кардіо.", DifficultyLevel.AboveAverage, TrainingType.Home, 10)
                { AgeRange = new List<int>{12, 60}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 45, MaxWeight = 115, MinHeight = 140, MaxHeight = 200, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.WeightLoss, WorkoutGoal.KeepFit } },

                new Exercise("Біг на біговій доріжці (ЛЕГКО)", "Розминка.", DifficultyLevel.Easy, TrainingType.Gym, 15)
                { AgeRange = new List<int>{12, 75}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 40, MaxWeight = 140, MinHeight = 140, MaxHeight = 220, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.HealthAndMobility, WorkoutGoal.WeightLoss } },

                new Exercise("Інтервальний біг на доріжці (HIIT)", "Спалювання жиру.", DifficultyLevel.Hard, TrainingType.Gym, 20)
                { AgeRange = new List<int>{16, 50}, RequiredLevel = FitnessLevelRequired.Pro, MinWeight = 55, MaxWeight = 110, MinHeight = 150, MaxHeight = 200, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.WeightLoss, WorkoutGoal.KeepFit } },

                new Exercise("Орбітрек (Еліпсоїд)", "Кардіо без ударного навантаження на коліна.", DifficultyLevel.Easy, TrainingType.Gym, 20)
                { AgeRange = new List<int>{14, 80}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 40, MaxWeight = 150, MinHeight = 140, MaxHeight = 220, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.HealthAndMobility, WorkoutGoal.WeightLoss } },

                new Exercise("Велотренажер", "Розвиток витривалості ніг.", DifficultyLevel.Easy, TrainingType.Gym, 20)
                { AgeRange = new List<int>{12, 80}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 40, MaxWeight = 160, MinHeight = 130, MaxHeight = 220, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.WeightLoss, WorkoutGoal.HealthAndMobility } },

                new Exercise("Гребний тренажер", "Кардіо з навантаженням на спину.", DifficultyLevel.AboveAverage, TrainingType.Gym, 15)
                { AgeRange = new List<int>{16, 65}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 50, MaxWeight = 130, MinHeight = 150, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.WeightLoss, WorkoutGoal.KeepFit } },

                new Exercise("Степпер", "Імітація підйому сходами.", DifficultyLevel.AboveAverage, TrainingType.Gym, 15)
                { AgeRange = new List<int>{14, 65}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 45, MaxWeight = 120, MinHeight = 140, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.WeightLoss, WorkoutGoal.KeepFit } },

                new Exercise("Бій з тінню", "Кардіо з елементами єдиноборств.", DifficultyLevel.Easy, TrainingType.Home, 10)
                { AgeRange = new List<int>{10, 70}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 40, MaxWeight = 130, MinHeight = 130, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.WeightLoss, WorkoutGoal.KeepFit } },

                new Exercise("Застрибування на тумбу", "Пліометрика для ніг.", DifficultyLevel.Hard, TrainingType.Gym, 10)
                { AgeRange = new List<int>{16, 45}, RequiredLevel = FitnessLevelRequired.Pro, MinWeight = 55, MaxWeight = 95, MinHeight = 160, MaxHeight = 200, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.Strength, WorkoutGoal.WeightLoss } },

                new Exercise("Махи гирею (Kettlebell Swings)", "Динаміка для заднього ланцюга.", DifficultyLevel.AboveAverage, TrainingType.Gym, 10)
                { AgeRange = new List<int>{16, 60}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 55, MaxWeight = 130, MinHeight = 150, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.Strength, WorkoutGoal.WeightLoss } },

                new Exercise("Кидки медболу в стіну", "Вибухова сила.", DifficultyLevel.AboveAverage, TrainingType.Gym, 10)
                { AgeRange = new List<int>{14, 55}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 50, MaxWeight = 120, MinHeight = 150, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.Strength, WorkoutGoal.KeepFit } },

                new Exercise("Канати (Battle Ropes)", "Інтенсивне кардіо для рук.", DifficultyLevel.Hard, TrainingType.Gym, 10)
                { AgeRange = new List<int>{18, 50}, RequiredLevel = FitnessLevelRequired.Pro, MinWeight = 60, MaxWeight = 120, MinHeight = 155, MaxHeight = 200, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.WeightLoss, WorkoutGoal.Strength } },

                // ==========================================
                // КАТЕГОРІЯ 7: СПЕЦИФІЧНІ / НІШЕВІ ВПРАВИ
                // ==========================================
                new Exercise("Вихід силою на дві руки", "Складний гімнастичний елемент.", DifficultyLevel.Hard, TrainingType.Gym, 15)
                { AgeRange = new List<int>{16, 40}, RequiredLevel = FitnessLevelRequired.Pro, MinWeight = 55, MaxWeight = 85, MinHeight = 150, MaxHeight = 190, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.Strength } },

                new Exercise("Присідання Пістолетик", "Присідання на одній нозі.", DifficultyLevel.Hard, TrainingType.Home, 10)
                { AgeRange = new List<int>{14, 45}, RequiredLevel = FitnessLevelRequired.Pro, MinWeight = 50, MaxWeight = 90, MinHeight = 140, MaxHeight = 190, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.Strength, WorkoutGoal.KeepFit } },

                new Exercise("Віджимання в стійці на руках", "Екстремальне навантаження на плечі.", DifficultyLevel.Hard, TrainingType.Home, 8)
                { AgeRange = new List<int>{16, 40}, RequiredLevel = FitnessLevelRequired.Pro, MinWeight = 55, MaxWeight = 85, MinHeight = 150, MaxHeight = 190, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.Strength } },

                new Exercise("Стійка на голові", "Баланс.", DifficultyLevel.AboveAverage, TrainingType.Home, 5)
                { AgeRange = new List<int>{14, 50}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 45, MaxWeight = 90, MinHeight = 140, MaxHeight = 190, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.HealthAndMobility, WorkoutGoal.KeepFit } },

                new Exercise("Ходьба фермера", "Зміцнення хвату та кору.", DifficultyLevel.AboveAverage, TrainingType.Gym, 10)
                { AgeRange = new List<int>{18, 60}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 60, MaxWeight = 150, MinHeight = 155, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.Strength, WorkoutGoal.MuscleGain } },

                new Exercise("Тяга саней", "Силова витривалість.", DifficultyLevel.Hard, TrainingType.Gym, 15)
                { AgeRange = new List<int>{18, 50}, RequiredLevel = FitnessLevelRequired.Pro, MinWeight = 65, MaxWeight = 140, MinHeight = 160, MaxHeight = 200, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.Strength, WorkoutGoal.WeightLoss } },

                new Exercise("Підйом штанги на груди (Clean)", "Важка атлетика.", DifficultyLevel.Hard, TrainingType.Gym, 15)
                { AgeRange = new List<int>{18, 45}, RequiredLevel = FitnessLevelRequired.Pro, MinWeight = 65, MaxWeight = 130, MinHeight = 160, MaxHeight = 200, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.Strength, WorkoutGoal.MuscleGain } },

                new Exercise("Ривок штанги (Snatch)", "Складнокоординаційна вправа.", DifficultyLevel.Hard, TrainingType.Gym, 20)
                { AgeRange = new List<int>{18, 45}, RequiredLevel = FitnessLevelRequired.Pro, MinWeight = 65, MaxWeight = 125, MinHeight = 160, MaxHeight = 195, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.Strength } },

                new Exercise("Жим Арнольда", "Модифікація жиму гантелей для плечей.", DifficultyLevel.AboveAverage, TrainingType.Gym, 10)
                { AgeRange = new List<int>{16, 60}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 55, MaxWeight = 130, MinHeight = 150, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.MuscleGain, WorkoutGoal.KeepFit } },

                new Exercise("Згинання Зоттмана", "Біцепс + передпліччя.", DifficultyLevel.AboveAverage, TrainingType.Gym, 8)
                { AgeRange = new List<int>{16, 60}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 55, MaxWeight = 130, MinHeight = 150, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.MuscleGain } },

                new Exercise("Розгинання на трицепс з мотузкою", "Ізоляція.", DifficultyLevel.Easy, TrainingType.Gym, 8)
                { AgeRange = new List<int>{14, 75}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 40, MaxWeight = 150, MinHeight = 140, MaxHeight = 220, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.KeepFit, WorkoutGoal.MuscleGain } },

                new Exercise("Крокодилячі віджимання", "Прес + груди в русі.", DifficultyLevel.Hard, TrainingType.Home, 10)
                { AgeRange = new List<int>{16, 45}, RequiredLevel = FitnessLevelRequired.Pro, MinWeight = 50, MaxWeight = 95, MinHeight = 150, MaxHeight = 195, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.KeepFit, WorkoutGoal.Strength } },

                new Exercise("Вправа 'Доброго ранку'", "Зміцнення попереку зі штангою.", DifficultyLevel.AboveAverage, TrainingType.Gym, 10)
                { AgeRange = new List<int>{18, 55}, RequiredLevel = FitnessLevelRequired.Intermediate, MinWeight = 60, MaxWeight = 130, MinHeight = 155, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.HealthAndMobility, WorkoutGoal.MuscleGain } },

                new Exercise("Підтягування на одній руці", "Екстремальна сила.", DifficultyLevel.Hard, TrainingType.Gym, 10)
                { AgeRange = new List<int>{18, 35}, RequiredLevel = FitnessLevelRequired.Pro, MinWeight = 55, MaxWeight = 85, MinHeight = 160, MaxHeight = 190, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.Strength } },

                // ==========================================
                // КАТЕГОРІЯ 8: СТРЕТЧИНГ ТА ЙОГА (ДІМ)
                // ==========================================
                new Exercise("Поза дитини", "Розслаблення спини.", DifficultyLevel.Easy, TrainingType.Home, 5)
                { AgeRange = new List<int>{5, 85}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 20, MaxWeight = 160, MinHeight = 100, MaxHeight = 230, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.HealthAndMobility, WorkoutGoal.KeepFit } },

                new Exercise("Собака мордою вниз", "Розтяжка задньої поверхні.", DifficultyLevel.Easy, TrainingType.Home, 5)
                { AgeRange = new List<int>{10, 75}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 35, MaxWeight = 140, MinHeight = 120, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.HealthAndMobility, WorkoutGoal.KeepFit } },

                new Exercise("Кішка-Корова", "Мобільність хребта.", DifficultyLevel.Easy, TrainingType.Home, 5)
                { AgeRange = new List<int>{8, 85}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 30, MaxWeight = 160, MinHeight = 110, MaxHeight = 220, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.HealthAndMobility, WorkoutGoal.KeepFit } },

                new Exercise("Поза кобри", "Розтяжка преса.", DifficultyLevel.Easy, TrainingType.Home, 5)
                { AgeRange = new List<int>{10, 75}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 35, MaxWeight = 150, MinHeight = 120, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.HealthAndMobility, WorkoutGoal.KeepFit } },

                new Exercise("Розтяжка грудних у дверному отворі", "Зняття спазму з грудей.", DifficultyLevel.Easy, TrainingType.Home, 5)
                { AgeRange = new List<int>{10, 80}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 30, MaxWeight = 160, MinHeight = 120, MaxHeight = 230, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.HealthAndMobility, WorkoutGoal.KeepFit } },

                new Exercise("Нахили до ніг сидячи", "Розтяжка біцепса стегна.", DifficultyLevel.Easy, TrainingType.Home, 5)
                { AgeRange = new List<int>{8, 80}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 30, MaxWeight = 150, MinHeight = 110, MaxHeight = 220, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.HealthAndMobility, WorkoutGoal.KeepFit } },

                new Exercise("Метелик (розтяжка)", "Привідні м'язи.", DifficultyLevel.Easy, TrainingType.Home, 5)
                { AgeRange = new List<int>{8, 80}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 30, MaxWeight = 150, MinHeight = 110, MaxHeight = 220, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.HealthAndMobility, WorkoutGoal.KeepFit } },

                new Exercise("Випад бігуна", "Розтяжка згинача стегна.", DifficultyLevel.Easy, TrainingType.Home, 5)
                { AgeRange = new List<int>{12, 70}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 40, MaxWeight = 140, MinHeight = 130, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.HealthAndMobility, WorkoutGoal.KeepFit } },

                new Exercise("Скручування хребта лежачи", "Декомпресія.", DifficultyLevel.Easy, TrainingType.Home, 5)
                { AgeRange = new List<int>{10, 80}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 35, MaxWeight = 160, MinHeight = 120, MaxHeight = 220, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.HealthAndMobility, WorkoutGoal.KeepFit } },

                new Exercise("Розтяжка трицепса за головою", "Зняття напруги з рук.", DifficultyLevel.Easy, TrainingType.Home, 5)
                { AgeRange = new List<int>{10, 85}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 30, MaxWeight = 160, MinHeight = 120, MaxHeight = 230, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.HealthAndMobility, WorkoutGoal.KeepFit } },

                new Exercise("Перекати на спині", "Масаж хребта.", DifficultyLevel.Easy, TrainingType.Home, 5)
                { AgeRange = new List<int>{8, 75}, RequiredLevel = FitnessLevelRequired.Beginner, MinWeight = 30, MaxWeight = 130, MinHeight = 110, MaxHeight = 210, SuitableGoals = new List<WorkoutGoal> { WorkoutGoal.HealthAndMobility, WorkoutGoal.KeepFit } }
            };
        }
    }
}