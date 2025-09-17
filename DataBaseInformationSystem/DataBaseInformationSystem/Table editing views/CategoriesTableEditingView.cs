using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace DataBaseInformationSystem {
    internal class CategoriesTableEditingView : TableEditingView {

        ObservableCollection<CategoryInfo> collection;

        public CategoriesTableEditingView() {
            init(new CategoriesTableView());

            collection = new ObservableCollection<CategoryInfo>(
                DataBaseManager.Instance.SelectCategories());

            tableView.SetItemsSource(collection);
        }

        public ObservableCollection<CategoryInfo> Collection() {
            return collection;
        }

        bool check(string text) {
            if (collection.Where(n => n.Name.Equals(text)).ToArray().Length > 0) {
                MessageBox.Show("Эта категория уже есть в таблице");
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

                CategoryInfo info = new CategoryInfo {
                    Id = DataBaseManager.Instance.InsertCategory(textEditingWindow.Text),
                    Name = textEditingWindow.Text
                };

                collection.Add(info);
            }
        }

        protected override void OnEditButtonPressed() {
            CategoryInfo info = collection[selectedIndex];

            TextEditingWindow textEditingWindow = new TextEditingWindow(info.Name);
            textEditingWindow.ShowDialog();

            if (info.Name.Equals(textEditingWindow.Text)) {
                return;
            }

            if (check(textEditingWindow.Text)) {
                return;
            }

            info.Name = textEditingWindow.Text;
            DataBaseManager.Instance.UpdateCategory(info.Id, info.Name);
        }

        protected override void OnDeleteButtonPressed() {
            CategoryInfo info = collection[selectedIndex];

            tableView.DeselectSelectedItem();

            collection.Remove(info);

            DataBaseManager.Instance.DeleteCategory(info.Id);
        }

        protected override void OnSelectedItemMouseDoubleClick() {
            base.OnSelectedItemMouseDoubleClick();
            OnEditButtonPressed();
        }
    }
}