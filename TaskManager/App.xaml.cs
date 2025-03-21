using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TaskManager {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    public partial class App : Application {

        public static int taskCounter = 0;
        public static int complitedTaskCounter = 0;

        public static Dictionary<int, string> data = new Dictionary<int, string>();
        private static MainWindow window = new MainWindow();

        public static AppDbContext DbContext { get; private set; }

        public class DeadlineItem {
            public int Id { get; set; }
            public required string DeadlineText { get; set; }
        }

        public App() {
            var host = Environment.GetEnvironmentVariable("PG_HOST");
            var port = Environment.GetEnvironmentVariable("PG_PORT");
            var username = Environment.GetEnvironmentVariable("PG_USERNAME");
            var password = Environment.GetEnvironmentVariable("PG_PASSWORD");
            var database = Environment.GetEnvironmentVariable("PG_DATABASE");
            var connectionString = $"Host={host};Port={port};Username={username};Password={password};Database={database}";
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql(connectionString);
            DbContext = new AppDbContext(optionsBuilder.Options);

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
            var data = App.DbContext.MyEntities.ToList();
            foreach (var entity in data) {
                Console.WriteLine(entity.Name);
            }
            window.UpdateProgressBar();
        }
        private void UpdateData() {
            //App.data
        }

    }

}
