using System;
using System.Collections.Generic;
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
            public bool IsComlete;
            public string DisplayText => $"{(IsComlete ? "[X]" : "[ ]")} {Name} ({Priority})";
        }

        private List<TaskItem> allTasks = new List<TaskItem>();

        public MainWindow()
        {
            InitializeComponent();
            allTasks.Add(new TaskItem {Name = "Купить продукты", Priority = "Высокий", IsComlete = true});
            allTasks.Add(new TaskItem { Name = "Позванить маме", Priority = "Высокий", IsComlete = false});
            allTasks.Add(new TaskItem { Name = "Сделать домашнюю работу", Priority = "Низкий", IsComlete = true});
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
        }

        private void CompleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SortBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FilterAllRbt_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
