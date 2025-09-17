using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace DataBaseInformationSystem {
    internal class EmployeesTableEditingView : TableEditingView {

        ObservableCollection<EmployeeInfo> collection;

        public EmployeesTableEditingView() {
            init(new EmployeesTableView());

            collection = new ObservableCollection<EmployeeInfo>(
                DataBaseManager.Instance.SelectEmployees());

            tableView.SetItemsSource(collection);
        }

        public ObservableCollection<EmployeeInfo> Collection() {
            return collection;
        }

        bool check(string text) {
            if (collection.Where(n => n.FullName.Equals(text)).ToArray().Length > 0) {
                MessageBox.Show("Этот сотрудник уже есть в таблице");
                return true;
            }
            return false;
        }

        protected override void OnAddButtonPressed() {
            tableView.DeselectSelectedItem();

            TextEditingWindow textEditingWindow = new TextEditingWindow();
            textEditingWindow.ShowDialog();

            if (textEditingWindow.Text != null && !textEditingWindow.Text.Equals(string.Empty)) {
                if (check(textEditingWindow.Text)) {
                    return;
                }

                EmployeeInfo info = new EmployeeInfo {
                    Id = DataBaseManager.Instance.InsertEmployee(textEditingWindow.Text),
                    FullName = textEditingWindow.Text
                };

                collection.Add(info);
            }
        }

        protected override void OnEditButtonPressed() {
            EmployeeInfo info = collection[selectedIndex];

            TextEditingWindow textEditingWindow = new TextEditingWindow(info.FullName);
            textEditingWindow.ShowDialog();

            if (info.FullName.Equals(textEditingWindow.Text)) {
                return;
            }

            if (check(textEditingWindow.Text)) {
                return;
            }

            info.FullName = textEditingWindow.Text;
            DataBaseManager.Instance.UpdateEmployee(info.Id, info.FullName);
        }

        protected override void OnDeleteButtonPressed() {
            EmployeeInfo info = collection[selectedIndex];

            tableView.DeselectSelectedItem();

            collection.Remove(info);

            DataBaseManager.Instance.DeleteEmployee(info.Id);
        }

        protected override void OnSelectedItemMouseDoubleClick() {
            base.OnSelectedItemMouseDoubleClick();
            OnEditButtonPressed();
        }
    }
}