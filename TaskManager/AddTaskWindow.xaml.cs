using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TaskManager {
    /// <summary>
    /// Логика взаимодействия для AddTaskWindow.xaml
    /// </summary>
    public partial class AddTaskWindow : Window {
        public string TaskTitle { get; private set; }
        public string TaskDescription { get; private set; }
        public DateTime TaskSelectedDate { get; private set; }

        SolidColorBrush errorBrush;
        SolidColorBrush normalBrush;

        public AddTaskWindow() {
            InitializeComponent();
            errorBrush = (SolidColorBrush)FindResource("ErrorColor");
            normalBrush = (SolidColorBrush)FindResource("BackgroundColor");
        }

        private void AddButtonClick(object sender, RoutedEventArgs e) {
            bool valid = true;
            if (TitleTextBox.Text.Length == 0 || TitleTextBox.Text.Length >= 64) {
                TitleTextBox.Background = errorBrush;
                valid = false;
            }
            else {
                TitleTextBox.Background = normalBrush;
            }
            if (DescriptionTextBox.Text.Length == 0 || DescriptionTextBox.Text.Length >= 256) {
                DescriptionTextBox.Background = errorBrush;
                valid = false;
            }
            else {
                DescriptionTextBox.Background = normalBrush;
            }
            string dateText = DatePicker.Text;
            if (!DateTime.TryParseExact(dateText, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)) {
                DatePicker.Background = errorBrush;
                DatePicker.SelectedDate = DateTime.Today;
                valid = false;
            }
            else { 
                DatePicker.Background = normalBrush;
            }

            if (valid) {
                TaskTitle = TitleTextBox.Text;
                TaskDescription = DescriptionTextBox.Text;
                TaskSelectedDate = DatePicker.SelectedDate ?? DateTime.Today;

                DialogResult = true;
                Close();
            }
            return;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e) {
            DialogResult = false;
            Close();
        }

    }
}
