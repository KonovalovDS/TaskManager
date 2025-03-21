using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager {
    public class Entity {
        public int Id { get; set; } // Идентификатор пользователя
        public required string Name { get; set; }  
        public required string Login { get; set; } // Логин пользователя
        public required string HashedPassword { get; set; } // Хэшированный пароль
        public required ICollection<Task> Tasks { get; set; } // Связь с задачами (каждый пользователь может иметь несколько задач)
    }
}
