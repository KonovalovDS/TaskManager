using System;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Media;

namespace TaskManager {
    public class AddTaskViewModel : INotifyPropertyChanged {
        private readonly AddTaskWindow _window;

        private string _title;
        private string _description;
        private DateTime? _deadline;

        private bool _isTitleValid;
        private bool _isDescriptionValid;
        private bool _isDeadlineValid;

        private Brush _titleBorderBrush;
        private Brush _descriptionBorderBrush;
        private Brush _deadlineBorderBrush;

        public string Title {
            get => _title;
            set {
                _title = value;
                OnPropertyChanged(nameof(Title));
                ValidateFields();
            }
        }

        public string Description {
            get => _description;
            set {
                _description = value;
                OnPropertyChanged(nameof(Description));
                ValidateFields();
            }
        }

        public DateTime? Deadline {
            get => _deadline;
            set {
                _deadline = value;
                OnPropertyChanged(nameof(Deadline));
                ValidateFields();
            }
        }

        public Brush TitleBorderBrush {
            get => _titleBorderBrush;
            set {
                _titleBorderBrush = value;
                OnPropertyChanged(nameof(TitleBorderBrush));
            }
        }

        public Brush DescriptionBorderBrush {
            get => _descriptionBorderBrush;
            set {
                _descriptionBorderBrush = value;
                OnPropertyChanged(nameof(DescriptionBorderBrush));
            }
        }

        public Brush DeadlineBorderBrush {
            get => _deadlineBorderBrush;
            set {
                _deadlineBorderBrush = value;
                OnPropertyChanged(nameof(DeadlineBorderBrush));
            }
        }

        public ICommand AddTaskCommand { get; }
        public ICommand CloseCommand { get; }

        public AddTaskViewModel(AddTaskWindow window) {
            _window = window;

            AddTaskCommand = new RelayCommand(AddTask);
            CloseCommand = new RelayCommand(CloseWindow);

            // Изначально поля имеют белый фон
            TitleBorderBrush = Brushes.Transparent;
            DescriptionBorderBrush = Brushes.Transparent;
            DeadlineBorderBrush = Brushes.Transparent;
        }

        private void ValidateFields() {
            _isTitleValid = !string.IsNullOrEmpty(Title);
            _isDescriptionValid = !string.IsNullOrEmpty(Description);
            _isDeadlineValid = Deadline.HasValue;

            // Подсветка полей в зависимости от валидности
            TitleBorderBrush = _isTitleValid ? Brushes.Transparent : Brushes.Red;
            DescriptionBorderBrush = _isDescriptionValid ? Brushes.Transparent : Brushes.Red;
            DeadlineBorderBrush = _isDeadlineValid ? Brushes.Transparent : Brushes.Red;
        }

        private void AddTask() {
            // Проверка на валидность всех полей
            if (!_isTitleValid || !_isDescriptionValid || !_isDeadlineValid) {
                return;
            }

            // Создаем задачу
            var newTask = new Task(Title, Description, Deadline.Value, 1); // Пример с UserId = 1

            // Добавляем задачу в основной список
            var mainViewModel = (MainViewModel)App.Current.MainWindow.DataContext;
            mainViewModel.Tasks.Insert(0, newTask);
            //mainViewModel.DictTasks.Add(newTask.TaskId, newTask);

            // Закрываем окно
            _window.Close();
        }

        private void CloseWindow() {
            _window.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
