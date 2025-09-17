using System.Windows;
using System.Collections.ObjectModel;
using System.Linq;

namespace DataBaseInformationSystem {
    internal class SoftwareTableEditingView : TableEditingView {

        ObservableCollection<SoftwareInfo> collection;

        public SoftwareTableEditingView() {
            init(new SoftwareTableView());

            Loaded += SoftwareTableEditingView_Loaded;
            Unloaded += SoftwareTableEditingView_Unloaded;
        }

        void SoftwareTableEditingView_Loaded(object sender, RoutedEventArgs e) {
            collection = new ObservableCollection<SoftwareInfo>(
                DataBaseManager.Instance.SelectSoftware());

            tableView.SetItemsSource(collection);
        }

        void SoftwareTableEditingView_Unloaded(object sender, RoutedEventArgs e) {
            collection = null;
        }

        public ObservableCollection<SoftwareInfo> Collection() {
            if (collection == null) {
                collection = new ObservableCollection<SoftwareInfo>(
                    DataBaseManager.Instance.SelectSoftware());
            }

            return collection;
        }

        protected override void OnAddButtonPressed() {
            tableView.DeselectSelectedItem();

            SoftwareEditingWindow window = new SoftwareEditingWindow();
            window.ShowDialog();

            if (window.SoftwareInfo != null) {
                collection.Add(window.SoftwareInfo);
            }
        }

        protected override void OnEditButtonPressed() {
            SoftwareInfo info = collection[selectedIndex];

            SoftwareEditingWindow window = new SoftwareEditingWindow(info);
            window.ShowDialog();
        }

        protected override void OnDeleteButtonPressed() {
            SoftwareInfo info = collection[selectedIndex];

            tableView.DeselectSelectedItem();

            collection.Remove(info);

            DataBaseManager.Instance.DeleteSoftware(info.Id);
        }

        protected override void OnSelectedItemMouseDoubleClick() {
            base.OnSelectedItemMouseDoubleClick();
            OnEditButtonPressed();
        }
    }
}