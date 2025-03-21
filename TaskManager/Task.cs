using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager {
    public class Task {
        public int Id { get; set; } // Идентификатор задачи
        public required string Title { get; set; } // Заголовок задачи
        public required string Description { get; set; } // Описание задачи
        public DateTime Deadline { get; set; } // Дедлайн задачи
        public bool IsCompleted { get; set; } // Статус задачи (выполнена или нет)
        public int UserId { get; set; } // Внешний ключ на пользователя
        public required User User { get; set; } // Навигационное свойство для пользователя
    }
}
