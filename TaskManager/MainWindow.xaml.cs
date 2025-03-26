using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static TaskManager.App;
using static System.Net.Mime.MediaTypeNames;

namespace TaskManager {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public class DeadlineItem {
            public Guid Id { get; set; }
            public string DeadlineText { get; set; }
            public string DeadlineDescription { get; set; }
        }
        private ObservableCollection<DeadlineItem> Deadlines { get; set; }

        public MainWindow() {
            InitializeComponent();
            var app = (App)System.Windows.Application.Current;
            Deadlines = new ObservableCollection<DeadlineItem>();
            DeadlineListBox.ItemsSource = Deadlines;
        }

        private App App;

        public void UpdateProgressBar() {
            if (App.complitedTaskCounter > App.taskCounter) {
                Console.WriteLine("Error: complitedTaskCounter > taskCounter");
            }
            else {
                if (App.taskCounter == 0) {
                    WeeklyProgressBar.Value = 30;
                }
                else {
                    WeeklyProgressBar.Value = App.complitedTaskCounter / App.taskCounter;
                }
            }
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e) {
            ScrollViewer scrollViewer = sender as ScrollViewer;
            if (scrollViewer != null) {
                scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - e.Delta / 3);
                e.Handled = true;
            }
        }

        private void AddTaskClick(object sender, RoutedEventArgs e) {
            AddTaskWindow addTaskWindow = new AddTaskWindow();
            if (addTaskWindow.ShowDialog() == true) {
                Guid taskId = App.AddTask(addTaskWindow.TaskTitle, addTaskWindow.TaskDescription, addTaskWindow.TaskSelectedDate);
                var newItem = new DeadlineItem {
                    Id = taskId,
                    DeadlineText = $"{addTaskWindow.TaskTitle} - {addTaskWindow.TaskSelectedDate:dd.MM}",
                    DeadlineDescription = addTaskWindow.TaskDescription
                };
                Deadlines.Add(newItem);
                //UpdateProgressBar();
            }
        }

        private void CompleteTaskClick(object sender, RoutedEventArgs e) {
            Guid taskId = getItemId(object sender, RoutedEventArgs e);
            if (taskId != Guid.Empty)
                        App.CompleteTask(taskId);
                        //UpdateProgressBar();
                    }
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e) {

            int id = 0;
            //var itemToRemove = Deadlines.FirstOrDefault(item => item.Id == id);
            //if (itemToRemove != null) {
                //App.DeleteTask(id);
                //Deadlines.Remove(itemToRemove);
            //}
        }

        private Guid getItemId(object sender, RoutedEventArgs e) {
            var button = sender as Button;
            if (button != null) {
                var listBoxItem = GetParentListBoxItem(button);
                if (listBoxItem != null) {
                    var deadlineItem = listBoxItem.DataContext as DeadlineItem;
                    if (deadlineItem != null) {
                        return deadlineItem.Id;
                    }
                }
            }
            return Guid.Empty;
        }

        private ListBoxItem GetParentListBoxItem(DependencyObject child) {
            while (child != null) {
                child = VisualTreeHelper.GetParent(child);
                if (child is ListBoxItem listBoxItem)
                    return listBoxItem;
            }
            return null;
        }

    }
}