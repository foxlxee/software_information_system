using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace DataBaseInformationSystem {
    internal class DevelopersTableEditingView : TableEditingView {

        ObservableCollection<DeveloperInfo> collection;

        public DevelopersTableEditingView() {
            init(new DevelopersTableView());

            collection = new ObservableCollection<DeveloperInfo>(
                DataBaseManager.Instance.SelectDevelopers());

            tableView.SetItemsSource(collection);
        }

        public ObservableCollection<DeveloperInfo> Collection() {
            return collection;
        }

        bool check(string text) {
            if (collection.Where(n => n.Name.Equals(text)).ToArray().Length > 0) {
                MessageBox.Show("Этот разработчик уже есть в таблице");
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

                DeveloperInfo info = new DeveloperInfo {
                    Id = DataBaseManager.Instance.InsertDeveloper(textEditingWindow.Text),
                    Name = textEditingWindow.Text
                };

                collection.Add(info);
            }
        }

        protected override void OnEditButtonPressed() {
            DeveloperInfo info = collection[selectedIndex];

            TextEditingWindow textEditingWindow = new TextEditingWindow(info.Name);
            textEditingWindow.ShowDialog();

            if (info.Name.Equals(textEditingWindow.Text)) {
                return;
            }

            if (check(textEditingWindow.Text)) {
                return;
            }

            info.Name = textEditingWindow.Text;
            DataBaseManager.Instance.UpdateDeveloper(info.Id, info.Name);
        }

        protected override void OnDeleteButtonPressed() {
            DeveloperInfo info = collection[selectedIndex];

            tableView.DeselectSelectedItem();

            collection.Remove(info);

            DataBaseManager.Instance.DeleteDeveloper(info.Id);
        }

        protected override void OnSelectedItemMouseDoubleClick() {
            base.OnSelectedItemMouseDoubleClick();
            OnEditButtonPressed();
        }
    }
}