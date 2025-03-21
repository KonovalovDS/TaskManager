using System.Collections.ObjectModel;
using System.Text;
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
        }

        public ObservableCollection<DeadlineItem> Deadlines { get; set; } = new ObservableCollection<DeadlineItem>();

        public void UpdateProgressBar() {
            if (App.complitedTaskCounter > App.taskCounter) {
                Console.WriteLine("Error: complitedTaskCounter > taskCounter");
            }
            else {
                WeeklyProgressBar.Value = App.complitedTaskCounter / App.taskCounter;
            }
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e) {
            ScrollViewer scrollViewer = sender as ScrollViewer;
            if (scrollViewer != null) {
                scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - e.Delta / 3);
                e.Handled = true;
            }
        }

        private void AddButtonClick(object sender, RoutedEventArgs e) {
            DateTime date = DateTime.Now;
            //string msg = ;
            string text = $"{date.Day}.{date.Month} - ";
            int id = 0;
            while (App.data.ContainsKey(id)) {
                id++;
            }
            App.AddTask(id, text);
            Deadlines.Add(new DeadlineItem { Id = id, DeadlineText = text });
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e) {
            int id = 0;
            var itemToRemove = Deadlines.FirstOrDefault(item => item.Id == id);
            if (itemToRemove != null) {
                App.DeleteTask(id);
                Deadlines.Remove(itemToRemove);
            }
        }

    }
}