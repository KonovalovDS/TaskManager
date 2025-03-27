using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager {
    public class Task : INotifyPropertyChanged {
        public Guid TaskId { get; set; } = Guid.NewGuid(); // Генерация уникального идентификатора
        public string Title { get; set; } // Заголовок задачи
        public string Description { get; set; } // Описание задачи
        public DateTime Deadline { get; set; } // Дедлайн задачи
        public string DeadlineText => Deadline.ToString("dd MMM yyyy");
        public bool _isCompleted { get; set; } = false; // Статус задачи (выполнена или нет)
        public int UserId { get; set; } // Внешний ключ на пользователя 

        public string TitleAndDate => $"{Title} - {Deadline:dd.MM}";

        public Task(string title, string description, DateTime deadline, int userId) {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Заголовок задачи не может быть пустым", nameof(title));

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Описание задачи не может быть пустым", nameof(description));

            Title = title;
            Description = description;
            Deadline = deadline;
            UserId = userId;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool IsCompleted {
            get => _isCompleted;
            set {
                if (_isCompleted != value) {
                    _isCompleted = value;
                    OnPropertyChanged(nameof(IsCompleted));  // Уведомляем об изменении
                }
            }
        }
    }
}
