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

        // додає змінну для зберігання поточного плану на рівні класу сторінки
        private UserPlan currentPlan;
        private string planPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "my_plan.json");

        private void LoadPlan()
        {
            string planPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "my_plan.json");

            if (File.Exists(planPath))
            {
                //план з JSON
                string json = File.ReadAllText(planPath);
                currentPlan = JsonSerializer.Deserialize<UserPlan>(json);

                if (currentPlan != null && currentPlan.Exercises != null)
                {
                    PageTitle.Text = "Список всіх вправ для тренувань";
                    ExercisesGridView.ItemsSource = currentPlan.Exercises; //   вправи у картки
                }
            }
            else
            {
                PageTitle.Text = "Ви ще не побудували план. Натисніть 'Побудувати план' у правому верхньому куті.";
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            // вправа
            var button = sender as Button;
            var exerciseToDelete = button?.DataContext as Exercise;

            if (exerciseToDelete != null && currentPlan != null)
            {
                // видаляємо з масиву
                currentPlan.Exercises.Remove(exerciseToDelete);

                // оновлення
                ExercisesGridView.ItemsSource = null;
                ExercisesGridView.ItemsSource = currentPlan.Exercises;

                // збр JSON файл
                string json = JsonSerializer.Serialize(currentPlan, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(planPath, json);
            }
        }


        private async void ClearPlanBtn_Click(object sender, RoutedEventArgs e)
{
    // діалог підтвердження
    ConfirmDeleteDialog.XamlRoot = this.Content.XamlRoot;
    await ConfirmDeleteDialog.ShowAsync();
}

private void ConfirmDeleteDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
{
    // видалення файлу
    if (File.Exists(planPath))
    {
        File.Delete(planPath);
    }

    // з пам'яті програми
    if (currentPlan != null)
    {
        currentPlan.Exercises.Clear();
    }

    // інтерфейс
    ExercisesGridView.ItemsSource = null;
    PageTitle.Text = "План очищено. Створіть новий!";
    
    // кнопка очищення прибрана
    ClearPlanBtn.Visibility = Visibility.Collapsed;
}
    }
}
