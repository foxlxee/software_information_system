using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DataBaseInformationSystem {
    internal partial class InstallEditingWindow : Window {

        public InstallInfo InstallInfo { get; private set; }

        ObservableCollection<SoftwareInfo> software;
        ObservableCollection<EmployeeInfo> employees;

        public InstallEditingWindow(InstallInfo installInfo = null) {
            InitializeComponent();

            InstallInfo = installInfo;

            software = MainWindow.Window.SoftwareTableEditingView.Collection();
            employees = MainWindow.Window.EmployeesTableEditingView.Collection();

            softwareComboBox.ItemsSource = software;
            employeeComboBox.ItemsSource = employees;

            if (installInfo != null) {
                roomsTextBox.Text = installInfo.Rooms.Replace(", ", " ");
                versionTextBox.Text = installInfo.Version;
                datePicker.SelectedDate = DateTime.Parse(installInfo.Date);

                for (int i = 0; i < employees.Count; i++) {
                    if (employees[i].FullName.Equals(installInfo.EmployeeFullName)) {
                        employeeComboBox.SelectedIndex = i;
                        break;
                    }
                }

                for (int i = 0; i < software.Count; i++) {
                    if (software[i].Name.Equals(installInfo.SoftwareName)) {
                        softwareComboBox.SelectedIndex = i;
                        break;
                    }
                }
            } else {
                datePicker.SelectedDate = DateTime.Now;
            }

            roomsTextBox.PreviewTextInput += TextBox_PreviewTextInput;
            doneButton.Click += Button_Click;
        }
        
        void Button_Click(object sender, RoutedEventArgs e) {
            string text = roomsTextBox.Text.Trim();
            if (text.Equals(string.Empty)) {
                MessageBox.Show("Введите кабинеты");
                roomsTextBox.Text = null;
                roomsTextBox.Focus();
                return;
            }

            if (softwareComboBox.SelectedIndex == -1) {
                MessageBox.Show("Выберите ПО");
                return;
            }

            text = versionTextBox.Text.Trim();
            if (text.Equals(string.Empty)) {
                MessageBox.Show("Введите версию");
                versionTextBox.Text = null;
                versionTextBox.Focus();
                return;
            }

            if (employeeComboBox.SelectedIndex == -1) {
                MessageBox.Show("Выберите сотрудника");
                return;
            }

            if (!datePicker.SelectedDate.HasValue) {
                MessageBox.Show("Введите дату установки");
                return;
            }

            SoftwareInfo softwareInfo = softwareComboBox.SelectedItem as SoftwareInfo;
            EmployeeInfo employeeInfo = employeeComboBox.SelectedItem as EmployeeInfo;

            text = string.Empty;
            HashSet<int> roomsSet = new HashSet<int>();
            string[] rooms = roomsTextBox.Text.Split(' ');
            for (int i = 0; i < rooms.Length; i++) {
                if (!rooms[i].Equals(string.Empty)) roomsSet.Add(int.Parse(rooms[i]));
            }
            int[] roomsArray = roomsSet.ToArray();
            for (int i = 0; i < roomsArray.Length; i++) {
                text += roomsArray[i];
                if ((i + 1) != roomsArray.Length) {
                    text += ", ";
                }
            }

            InstallInfo newInstallInfo = new InstallInfo {
                SoftwareName = softwareInfo.Name,
                SoftwareCategory = softwareInfo.Category,
                SoftwareDeveloper = softwareInfo.Developer,
                Version = versionTextBox.Text,
                Date = datePicker.SelectedDate.Value.ToString("dd.MM.yyyy"),
                EmployeeFullName = employeeInfo.FullName,
                Rooms = text
            };

            if (InstallInfo != null) {
                if (newInstallInfo.Compare(InstallInfo)) {
                    InstallInfo = null;
                    Close();
                    return;
                }

                if (DataBaseManager.Instance.UpdateInstall(
                    InstallInfo.Id,
                    softwareInfo.Id,
                    employeeInfo.Id,
                    newInstallInfo.Version,
                    datePicker.SelectedDate.Value)) {

                    DataBaseManager.Instance.DeleteInstallRooms(InstallInfo.Id);

                    HashSet<int> roomsToAdd = newInstallInfo.GetRooms();
                    foreach (int item in roomsToAdd) DataBaseManager.Instance.InsertInstallRoom(InstallInfo.Id, item);
                }

                InstallInfo.Copy(newInstallInfo);

                Close();
                return;
            }

            int id = DataBaseManager.Instance.InsertInstall(
                softwareInfo.Id,
                employeeInfo.Id,
                versionTextBox.Text,
                datePicker.SelectedDate.Value);

            roomsArray = newInstallInfo.GetRooms().ToArray();
            for (int i = 0; i < roomsArray.Length; i++) {
                DataBaseManager.Instance.InsertInstallRoom(id, roomsArray[i]);
            }

            newInstallInfo.Id = id;

            InstallInfo = newInstallInfo;

            Close();
        }

        void TextBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e) {
            if (e.Command == ApplicationCommands.Paste) {
                e.Handled = true;
            }
        }

        void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            foreach (char c in e.Text) {
                if (!char.IsDigit(c) && c != ' ') {
                    e.Handled = true;
                    return;
                }
            }
        }

        protected override void OnKeyDown(KeyEventArgs e) {
            base.OnKeyDown(e);

            switch (e.Key) {
                case Key.Escape:
                    InstallInfo = null;
                    Close();
                    return;
                case Key.Enter:
                    Button_Click(null, null);
                    return;
            }
        }
    }
}