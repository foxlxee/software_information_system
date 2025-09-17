using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace DataBaseInformationSystem {
    internal class InstallsTableEditingView : TableEditingView {

        ObservableCollection<InstallInfo> collection;
        ObservableCollection<InstallInfo> filteredCollection;

        public InstallsTableEditingView() {
            init(new InstallsTableView(), true);

            Loaded += InstallsTableEditingView_Loaded;
            Unloaded += InstallsTableEditingView_Unloaded;
        }

        void InstallsTableEditingView_Loaded(object sender, RoutedEventArgs e) {
            collection = сollection();

            tableView.SetItemsSource(collection);

            addFiltersButton.IsEnabled = collection.Count != 0;
        }

        void InstallsTableEditingView_Unloaded(object sender, RoutedEventArgs e) {
            collection = filteredCollection = null;

            addFiltersButton.Content = "Установить фильтрацию";
            addFiltersButton.IsEnabled = false;
        }

        ObservableCollection<InstallInfo> сollection() {
            return new ObservableCollection<InstallInfo>(
                    DataBaseManager.Instance.SelectInstalls().OrderBy(n => n.Id));
        }

        public ObservableCollection<InstallInfo> Collection() {
            if (collection == null) collection = сollection();
            return collection;
        }

        protected override void OnAddButtonPressed() {
            tableView.DeselectSelectedItem();

            InstallEditingWindow window = new InstallEditingWindow();
            window.ShowDialog();

            if (window.InstallInfo != null) {
                collection.Add(window.InstallInfo);
            }

            addFiltersButton.IsEnabled = collection.Count != 0;
        }

        protected override void OnEditButtonPressed() {
            InstallInfo info = filteredCollection == null ? collection[selectedIndex] : filteredCollection[selectedIndex];

            InstallEditingWindow window = new InstallEditingWindow(info);
            window.ShowDialog();
        }

        protected override void OnDeleteButtonPressed() {
            InstallInfo info = filteredCollection == null ? collection[selectedIndex] : filteredCollection[selectedIndex];

            tableView.DeselectSelectedItem();

            collection.Remove(info);

            if (filteredCollection != null) {
                filteredCollection.Remove(info);
            } else {
                addFiltersButton.IsEnabled = collection.Count != 0;
            }

            DataBaseManager.Instance.DeleteInstall(info.Id);
        }

        protected override void OnAddFiltersButtonPressed() {
            base.OnAddFiltersButtonPressed();

            tableView.DeselectSelectedItem();

            if (filteredCollection != null) {
                filteredCollection = null;
                tableView.SetItemsSource(collection);

                addFiltersButton.Content = "Установить фильтрацию";
                addFiltersButton.IsEnabled = collection.Count != 0;

                addButton.IsEnabled = true;
                return;
            }

            FiltersSettingWindow filtersSettingWindow = new FiltersSettingWindow();
            filtersSettingWindow.ShowDialog();

            if (filtersSettingWindow.Filters != null) {

                var filtered = collection.Where(n => {
                    DateTime dt = DateTime.Parse(n.Date);
                    return dt >= filtersSettingWindow.Filters.FromDateTime && dt <= filtersSettingWindow.Filters.ToDateTime;
                });

                if (filtersSettingWindow.Filters.Employees != null) {
                    filtered = filtered.Where(n => {
                        return filtersSettingWindow.Filters.Employees.Any(
                            m => m.FullName.Equals(n.EmployeeFullName));
                    });
                }

                if (filtersSettingWindow.Filters.Software != null) {
                    filtered = filtered.Where(n => {
                        return filtersSettingWindow.Filters.Software.Any(
                            m => m.Name.Equals(n.SoftwareName));
                    });
                }

                if (filtersSettingWindow.Filters.Rooms != null) {
                    filtered = filtered.Where(n => {
                        return filtersSettingWindow.Filters.Rooms.Any(
                            m => n.GetRooms().Contains(m));
                    });
                }

                filteredCollection = new ObservableCollection<InstallInfo>(filtered);
                tableView.SetItemsSource(filteredCollection);

                addFiltersButton.Content = "Удалить фильтрацию";
                addButton.IsEnabled = false;
            }
        }

        protected override void OnSelectedItemMouseDoubleClick() {
            base.OnSelectedItemMouseDoubleClick();
            OnEditButtonPressed();
        }
    }
}