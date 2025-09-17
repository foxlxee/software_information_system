using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace DataBaseInformationSystem {
    internal abstract class TableEditingView : UserControl {

        protected TableView tableView;
        protected Button addButton;
        protected Button editButton;
        protected Button deleteButton;
        protected Button addFiltersButton;
        protected int selectedIndex;

        protected void init(TableView tableView, bool addFilters = false) {
            this.tableView = tableView;

            DockPanel dp = new DockPanel();
            Content = dp;

            addButton = new Button {
                Content = "Добавить"
            };
            editButton = new Button {
                Content = "Изменить",
                IsEnabled = false
            };
            deleteButton = new Button {
                Content = "Удалить",
                IsEnabled = false
            };

            UniformGrid grid = new UniformGrid {
                Rows = 1
            };
            DockPanel.SetDock(grid, Dock.Bottom);

            grid.Children.Add(addButton);
            grid.Children.Add(editButton);
            grid.Children.Add(deleteButton);

            if (addFilters) {
                addFiltersButton = new Button {
                    Content = "Установить фильтрацию"
                };
                addFiltersButton.Click += AddFiltersButton_Click;
                grid.Children.Add(addFiltersButton);
            }

            dp.Children.Add(grid);
            dp.Children.Add(tableView);

            tableView.ItemSelected += TableView_ItemSelected;
            tableView.ItemDeselected += TableView_ItemDeselected;
            tableView.MouseDoubleClick += TableView_MouseDoubleClick;

            addButton.Click += AddButton_Click;
            editButton.Click += EditButton_Click;
            deleteButton.Click += DeleteButton_Click;

            Unloaded += TableEditingView_Unloaded;
        }

        void TableEditingView_Unloaded(object sender, RoutedEventArgs e) {
            tableView.DeselectSelectedItem();
        }

        protected virtual void OnAddButtonPressed() { }
        protected virtual void OnEditButtonPressed() { }
        protected virtual void OnDeleteButtonPressed() { }
        protected virtual void OnAddFiltersButtonPressed() { }
        protected virtual void OnSelectedItemMouseDoubleClick() { }

        #region Buttons

        void AddButton_Click(object sender, RoutedEventArgs e) {
            OnAddButtonPressed();
        }

        void EditButton_Click(object sender, RoutedEventArgs e) {
            OnEditButtonPressed();
        }

        void DeleteButton_Click(object sender, RoutedEventArgs e) {
            OnDeleteButtonPressed();
        }

        void AddFiltersButton_Click(object sender, RoutedEventArgs e) {
            OnAddFiltersButtonPressed();
        }

        #endregion

        #region TableView

        void TableView_ItemSelected(int index) {
            editButton.IsEnabled = deleteButton.IsEnabled = true;
            selectedIndex = index;
        }

        void TableView_ItemDeselected(int index) {
            editButton.IsEnabled = deleteButton.IsEnabled = false;
            selectedIndex = index;
        }

        void TableView_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            DependencyObject obj = (DependencyObject)e.OriginalSource;

            while (obj != null && !(obj is DataGridRow)) {
                obj = VisualTreeHelper.GetParent(obj);
            }

            if (obj is DataGridRow) {
                DataGridRow row = (DataGridRow)obj;
                var clickedItem = row.Item;
                if (clickedItem != null && clickedItem == tableView.SelectedItem()) {
                    OnSelectedItemMouseDoubleClick();
                }
            }
        }

        #endregion
    }
}