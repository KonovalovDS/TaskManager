using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager {
    public class Entity {
        public int Id { get; set; } // Идентификатор пользователя

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Имя должно быть от 3 до 100 символов.")]
        public string Name { get; set; }  // Имя пользователя

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Логин должен быть от 3 до 50 символов.")]
        public string Login { get; set; } // Логин пользователя

        [Required]
        [StringLength(200, ErrorMessage = "Хэшированный пароль не может превышать 200 символов.")]
        public string HashedPassword { get; set; } // Хэшированный пароль

        public ICollection<Task> Tasks { get; set; } // Связь с задачами (каждый пользователь может иметь несколько задач)

        public Entity() {
            Tasks = new List<Task>(); // Инициализация коллекции задач
        }
    }
}
