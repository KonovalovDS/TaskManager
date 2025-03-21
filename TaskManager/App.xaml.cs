using System.Configuration;
using System.Data;
using System.Windows;

namespace TaskManager {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {

        public static int taskCounter = 0;
        public static int complitedTaskCounter = 0;

        public static Dictionary<int, string> data = new Dictionary<int, string>();
        private static MainWindow window = new MainWindow();

        public class DeadlineItem {
            public int Id { get; set; }
            public required string DeadlineText { get; set; }
        }

        public App() {
            InitializeComponent();
        }

        public static void AddTask(int id, string text) {
            App.taskCounter++;
            App.data.Add(id, text);
            window.UpdateProgressBar();
        }

        public static void DeleteTask(int id) {
            if (App.taskCounter > 0) { App.taskCounter--; }
            App.data.Remove(id);
            window.UpdateProgressBar();

        }

        public static void CompleteTask(int id) {
            DeleteTask(id);
            App.complitedTaskCounter++;
            window.UpdateProgressBar();
        }

        private void LoadData() {
            App.data.Clear();
            //App.data = getData();
            window.UpdateProgressBar();
        }
        private void UpdateData() {
            //App.data
        }

    }

}
