using System;
using System.Collections;
using System.Windows.Controls;
using System.Windows.Data;

namespace DataBaseInformationSystem {
    internal abstract class TableView : UserControl {

        public event Action<int> ItemSelected;
        public event Action<int> ItemDeselected;

        protected DataGrid dataGrid;

        protected TableView() {
            dataGrid = new DataGrid();
            Content = dataGrid;

            dataGrid.SelectedCellsChanged += DataGrid_SelectedCellsChanged;
        }

        void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e) {
            if (dataGrid.SelectedItem != null) {
                ItemSelected?.Invoke(dataGrid.SelectedIndex);
                return;
            }

            ItemDeselected?.Invoke(dataGrid.SelectedIndex);
        }
        
        protected void initColumns(Tuple<string, string>[] columns) {
            foreach (var tuple in columns) {
                dataGrid.Columns.Add(new DataGridTextColumn {
                    Header = tuple.Item1,
                    Binding = new Binding(tuple.Item2),
                    Width = new DataGridLength(1, DataGridLengthUnitType.Star)
                });
            }
        }

        public void SetItemsSource(IEnumerable itemsSource) {
            dataGrid.ItemsSource = itemsSource;
        }

        public IEnumerable GetItemsSource() {
            return dataGrid.ItemsSource;
        }

        public object SelectedItem() {
            return dataGrid.SelectedItem;
        }

        public void DeselectSelectedItem() {
            if (dataGrid.SelectedItem == null) return;

            dataGrid.SelectedItem = null;
        }
    }
}