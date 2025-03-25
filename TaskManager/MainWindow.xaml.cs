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

namespace TaskManager {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            var app = (App)Application.Current;
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
                App.AddTask(addTaskWindow.TaskTitle, addTaskWindow.TaskDescription, addTaskWindow.TaskSelectedDate);
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

    }
}