using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace TaskManager {
    public class MainViewModel : INotifyPropertyChanged {
        public ObservableCollection<Task> Tasks { get; set; }
        //public Dictionary<Guid, Task> DictTasks { get; set; }
        public ICommand EditTaskCommand { get; set; }
        public ICommand DeleteTaskCommand { get; set; }
        public ICommand CompleteTaskCommand { get; set; }
        public ICommand SortTasksCommand { get; set; }
        public ICommand OpenAddTaskWindowCommand { get; set; }

        private double _scrollPosition;
        public double ScrollPosition {
            get => _scrollPosition;
            set {
                _scrollPosition = value;
                OnPropertyChanged(nameof(ScrollPosition));
            }
        }

        private int _completedTaskCount = 0;
        private int _totalTaskCount = 0;

        public MainViewModel() {
            Tasks = new ObservableCollection<Task>
            {
            new Task("Задача 1", "Описание задачи 1", DateTime.Now.AddDays(1), 1),
            new Task("Задача 2", "Описание задачи 2", DateTime.Now.AddDays(2), 1),
            };
            _totalTaskCount = 2;

            //DictTasks = new Dictionary<Guid, Task>();

            // Команды
            DeleteTaskCommand = new RelayCommand(DeleteTask);
            CompleteTaskCommand = new RelayCommand(CompleteTask);
            SortTasksCommand = new RelayCommand(SortTasks);
            EditTaskCommand = new RelayCommand(EditTask);
            OpenAddTaskWindowCommand = new RelayCommand(OpenAddTaskWindow);
        }

        private void EditTask(object obj) {
            //
        }

        // Удаление задачи
        private void DeleteTask(object obj) {
            if (obj is Task task) {
                Tasks.Remove(task);
                _totalTaskCount--;
                OnPropertyChanged(nameof(Tasks)); // Уведомляем, что список обновился
            }
        }

        private void CompleteTask(object obj) {
            if (obj is Task task) {
                task.IsCompleted = true;
                _completedTaskCount++;
                Tasks.Remove(task);
                Tasks.Insert(_totalTaskCount - _completedTaskCount, task);
                OnPropertyChanged(nameof(Tasks)); // Уведомляем, что список обновился
            }
        }

        // Сортировка задач по дедлайну
        private void SortTasks() {
            var sortedTasks = Tasks.OrderBy(task => task.IsCompleted).ThenBy(task => task.Deadline).ToList();
            Tasks = new ObservableCollection<Task>(sortedTasks);
            OnPropertyChanged(nameof(Tasks));
        }

        private void OpenAddTaskWindow() {
            var addTaskWindow = new AddTaskWindow();
            addTaskWindow.ShowDialog();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // Для привязки выбранной задачи (например, для удаления)
        private Task _selectedTask;
        public Task SelectedTask {
            get => _selectedTask;
            set {
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
            }
        }

        protected virtual void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
