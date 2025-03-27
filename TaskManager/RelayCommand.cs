using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TaskManager {
    public class RelayCommand : ICommand {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        // Конструктор для команд с параметром
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null) {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        // Конструктор для команд без параметра
        public RelayCommand(Action execute, Func<bool> canExecute = null) {
            if (execute == null) throw new ArgumentNullException(nameof(execute));
            _execute = _ => execute();
            _canExecute = canExecute == null ? (Func<object, bool>)null : _ => canExecute();
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);
        public void Execute(object parameter) => _execute(parameter);

        public event EventHandler CanExecuteChanged {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
