using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Text.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SmartTrain
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WorkoutPage : Page
    {
        public WorkoutPage()
        {
            this.InitializeComponent();
            LoadPlan();
        }

        // Додаємо змінну для зберігання поточного плану на рівні класу сторінки
        private UserPlan currentPlan;
        private string planPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "my_plan.json");

        private void LoadPlan()
        {
            string planPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "my_plan.json");

            if (File.Exists(planPath))
            {
                // Читаємо план з JSON
                string json = File.ReadAllText(planPath);
                currentPlan = JsonSerializer.Deserialize<UserPlan>(json);

                if (currentPlan != null && currentPlan.Exercises != null)
                {
                    PageTitle.Text = "Список всіх вправ для тренувань"; // Ставимо назву плану в заголовок
                    ExercisesGridView.ItemsSource = currentPlan.Exercises; // Завантажуємо вправи у картки
                }
            }
            else
            {
                PageTitle.Text = "Ви ще не побудували план. Натисніть 'Побудувати план' у правому верхньому куті.";
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            // Отримуємо вправу, прив'язану до картки, на якій натиснули кнопку
            var button = sender as Button;
            var exerciseToDelete = button?.DataContext as Exercise;

            if (exerciseToDelete != null && currentPlan != null)
            {
                // 1. Видаляємо з масиву
                currentPlan.Exercises.Remove(exerciseToDelete);

                // 2. Оновлюємо відображення на екрані
                // (Перепризначаємо ItemsSource, щоб GridView побачив зміни)
                ExercisesGridView.ItemsSource = null;
                ExercisesGridView.ItemsSource = currentPlan.Exercises;

                // 3. Перезаписуємо JSON файл
                string json = JsonSerializer.Serialize(currentPlan, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(planPath, json);
            }
        }


        private async void ClearPlanBtn_Click(object sender, RoutedEventArgs e)
{
    // Показуємо діалог підтвердження
    ConfirmDeleteDialog.XamlRoot = this.Content.XamlRoot;
    await ConfirmDeleteDialog.ShowAsync();
}

private void ConfirmDeleteDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
{
    // 1. Видаляємо файл плану з пристрою
    if (File.Exists(planPath))
    {
        File.Delete(planPath);
    }

    // 2. Очищуємо дані в пам'яті програми
    if (currentPlan != null)
    {
        currentPlan.Exercises.Clear();
    }

    // 3. Оновлюємо інтерфейс
    ExercisesGridView.ItemsSource = null;
    PageTitle.Text = "План очищено. Створіть новий!";
    
    // Ховаємо кнопку очищення, бо план вже порожній
    ClearPlanBtn.Visibility = Visibility.Collapsed;
}
    }
}
