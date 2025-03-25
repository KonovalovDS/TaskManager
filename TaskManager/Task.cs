using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager {
    public class Task {
        public Guid TaskId { get; set; } = Guid.NewGuid(); // Генерация уникального идентификатора
        public required string Title { get; set; } // Заголовок задачи
        public required string Description { get; set; } // Описание задачи
        public DateTime Deadline { get; set; } // Дедлайн задачи
        public bool IsCompleted { get; set; } = false; // Статус задачи (выполнена или нет)
        public int UserId { get; set; } // Внешний ключ на пользователя
    }
}
