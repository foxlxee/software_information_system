using System;
using System.Linq;
using System.Windows;

namespace DataBaseInformationSystem {
    internal partial class FiltersSettingWindow : Window {

        public Filters Filters { get; private set; }

        public FiltersSettingWindow() {
            InitializeComponent();

            employeesListView.ItemsSource = MainWindow.Window.EmployeesTableEditingView.Collection();
            softwareListView.ItemsSource = MainWindow.Window.SoftwareTableEditingView.Collection();
            roomsListView.ItemsSource = DataBaseManager.Instance.SelectRooms();

            DateTime? minDate = DataBaseManager.Instance.SelectMinDateTime();
            fromDatePicker.SelectedDate = minDate != null ? minDate.Value : DateTime.Now;
            toDatePicker.SelectedDate = DateTime.Now;

            applyButton.Click += ApplyButton_Click;
        }

        void ApplyButton_Click(object sender, RoutedEventArgs e) {
            if (fromDatePicker.SelectedDate.Value > toDatePicker.SelectedDate.Value) {
                MessageBox.Show("Неверно введены даты");
                return;
            }

            Filters = new Filters {
                Employees = employeesListView.SelectedItems.Count != 0 ?
                employeesListView.SelectedItems.Cast<EmployeeInfo>().ToArray() : null,

                Software = softwareListView.SelectedItems.Count != 0 ?
                softwareListView.SelectedItems.Cast<SoftwareInfo>().ToArray() : null,

                Rooms = roomsListView.SelectedItems.Count != 0 ? roomsListView.SelectedItems.Cast<int>().ToArray() : null,

                FromDateTime = fromDatePicker.SelectedDate.Value,
                ToDateTime = toDatePicker.SelectedDate.Value
            };

            Close();
        }
    }
}