using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace TaskManager {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    public partial class App : Application {
        public App() {
            //ConnectDB();
            Data = new Dictionary<Guid, Task>();
            Order = new List<Guid>();

            InitializeComponent();
            window = new MainWindow();
        }

        public static int userId = 1;

        public static int taskCounter = 0;
        public static int complitedTaskCounter = 0;

        public static Dictionary<Guid, Task> Data { private get; set; }
        public static List<Guid> Order { private get; set; }

        private MainWindow window;

        struct DbContextStruct {
            public static AppDbContext DbContext { get; set; }
            static string host = Environment.GetEnvironmentVariable("PG_HOST");
            static string port = Environment.GetEnvironmentVariable("PG_PORT");
            static string username = "TestUser";
            static string password = "12345678";
            static string database = "tmdatabase";
            static public string connectionString = $"Host={host};Port={port};Username={username};Password={password};Database={database}";
        }

        void ConnectDB() {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql(DbContextStruct.connectionString);
            DbContextStruct.DbContext = new AppDbContext(optionsBuilder.Options);
            Debug.WriteLine(DbContextStruct.connectionString);
            try {
                using (var conn = new NpgsqlConnection(DbContextStruct.connectionString)) {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand("SELECT version();", conn)) {
                        var version = cmd.ExecuteScalar();
                        Debug.WriteLine($"Подключение успешно! Версия базы данных: {version}");
                    }
                }
            }
            catch (Exception ex) {
                Debug.WriteLine($"Ошибка подключения: {ex.Message}");
            }
        }

        public Guid AddTask(string taskTitle, string taskDecription, DateTime TaskDateTime) {
            taskCounter++;
            Task newTask = new Task {
                Title = taskTitle,
                Description = taskDecription,
                Deadline = TaskDateTime,
                UserId = userId
            };
            Data.Add(newTask.TaskId, newTask);
            Order.Add(newTask.TaskId);
            return newTask.TaskId;
        }

        public bool DeleteTask(Guid taskId) {
            if (Data.ContainsKey(taskId)) {
                Task task = Data[taskId];
                Order.Remove(taskId);
                Data.Remove(taskId);
                taskCounter--;
                return true;
            }
            return false;
        }

        public bool CompleteTask(Guid taskId) {
            if (Data.ContainsKey(taskId)) {
                Data[taskId].IsCompleted = true;
                Order.Remove(taskId);
                Order.Add(taskId);
                complitedTaskCounter++;
                return true;
            }
            return false;
        }

        private void LoadData() {
            App.Data.Clear();
            var data = App.DbContextStruct.DbContext.MyEntities.ToList();
            foreach (var entity in data) {
                Console.WriteLine(entity.Name);
            }
        }
        private void UpdateData() {
            //App.data
        }

        private void Button_Click(object sender, RoutedEventArgs e) {

        }
    }

}
