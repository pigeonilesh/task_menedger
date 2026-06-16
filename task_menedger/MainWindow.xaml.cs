using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace task_menedger
{
    public partial class MainWindow : Window
    {
        public class TaskItem
        {
            public string Name;
            public string Priority;
            public bool IsComleted;
            public string DisplayText => $"{(IsComleted ? "[X]" : "[ ]")} {Name} ({Priority})";
        }

        private List<TaskItem> allTasks = new List<TaskItem>();

        public MainWindow()
        {
            InitializeComponent();
            allTasks.Add(new TaskItem {Name = "Купить продукты", Priority = "Высокий", IsComleted = true});
            allTasks.Add(new TaskItem { Name = "Позванить маме", Priority = "Высокий", IsComleted = false});
            allTasks.Add(new TaskItem { Name = "Сделать домашнюю работу", Priority = "Низкий", IsComleted = true});

            UpdateDisplay();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            string taskName = TaskNameTB.Text.Trim();
            
            if (string.IsNullOrWhiteSpace(taskName))
            {
                MessageBox.Show("Введите название задания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ComboBoxItem selectedPriority = PriorityCmb.SelectedItem as ComboBoxItem;
            string priority = selectedPriority.Content.ToString();

            TaskItem task = new TaskItem
            {
                Name = taskName,
                Priority = priority,
                IsComleted = false
            };

            allTasks.Add(task);
            TaskNameTB.Clear();
            MessageBox.Show("Задача " + taskName + " успешно добавлена");

            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            List<TaskItem> tasks = GetFilteredTasks(); 
            TaskLb.Items.Clear();
            foreach (var task in tasks)
            {
                TaskLb.Items.Add(task.DisplayText);
            }

            int total = allTasks.Count;
            int completed = allTasks.Count(t => t.IsComleted == true);
            CounterTb.Text = $"Всего: {total} | Выполнено: {completed}";
        }

        private void CompleteBtn_Click(object sender, RoutedEventArgs e)
        {
            TaskItem selectedTask = GetSelectedTask();
            if (selectedTask == null)
            {
                MessageBox.Show("Выберите задачу!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (selectedTask.IsComleted)
            {
                MessageBox.Show("Задача уже выполнена", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            selectedTask.IsComleted = true;
            UpdateDisplay();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            TaskItem selectedTask = GetSelectedTask();
            if (selectedTask == null)
            {
                MessageBox.Show("Выберите задачу!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            allTasks.Remove(selectedTask);
            UpdateDisplay();
        }

        private void SortBtn_Click(object sender, RoutedEventArgs e)
        {
            List<TaskItem> result = new List<TaskItem>();
            foreach (var task in allTasks)
            {
                if (task.Priority == "Высокий")
                    result.Add(task);
            }
            foreach (var task in allTasks)
            {
                if (task.Priority == "Средний")
                    result.Add(task);
            }
            foreach (var task in allTasks)
            {
                if (task.Priority == "Низкий")
                    result.Add(task);
            }

            allTasks = result;
            UpdateDisplay();
        }

        private void FilterAllRbt_Click(object sender, RoutedEventArgs e)
        {
            UpdateDisplay();
        }

        private List<TaskItem> GetFilteredTasks()
        {
            List<TaskItem> result = new List<TaskItem>();
            foreach (var task in allTasks)
            {
                if (FilterAllRbt.IsChecked == true)
                {
                    result.Add(task);
                }
                else if (FilterHighRbt.IsChecked == true && task.Priority == "Высокий")
                {
                    result.Add(task);
                }
                else if (FilterMediumRbt.IsChecked == true && task.Priority == "Средний")
                {
                    result.Add(task);
                }
                else if (FilterLowRbt.IsChecked == true && task.Priority == "Низкий")
                {
                    result.Add(task);
                }
            }
            return result;
        }

        private TaskItem GetSelectedTask()
        {
            if (TaskLb.SelectedIndex == -1)
                return null;

            string selectedText = TaskLb.SelectedItem.ToString();

            foreach (var task in allTasks)
            {
                if (task.DisplayText == selectedText)
                    return task;
            }
            return null;
        }
    }
}
