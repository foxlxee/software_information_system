using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DataBaseInformationSystem {
    internal class MainWindow : Window {

        public static MainWindow Window { get; private set; }
        public readonly DevelopersTableEditingView DevelopersTableEditingView;
        public readonly CategoriesTableEditingView CategoriesTableEditingView;
        public readonly EmployeesTableEditingView EmployeesTableEditingView;
        public readonly SoftwareTableEditingView SoftwareTableEditingView;
        public readonly InstallsTableEditingView InstallsTableEditingView;

        Grid grid;
        SideBarView sideBarView;
        Border border;

        public MainWindow() {
            Window = this;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Width = 1200;
            Height = 600;
            MinWidth = 610;
            MinHeight = 350;

            grid = new Grid();
            Content = grid;

            grid.ColumnDefinitions.Add(new ColumnDefinition { MinWidth = 100, MaxWidth = 550, Width = new GridLength(250, GridUnitType.Pixel) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(5) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });

            sideBarView = new SideBarView(SideBarButton.Installs);
            sideBarView.SelectionChanged += SideBarView_SelectionChanged;
            Grid.SetColumn(sideBarView, 0);
            grid.Children.Add(sideBarView);
            
            GridSplitter splitter = new GridSplitter {
                Width = 4,
                Background = new SolidColorBrush(Color.FromRgb(230, 230, 230)),
                ResizeBehavior = GridResizeBehavior.PreviousAndNext,
            };
            Grid.SetColumn(splitter, 1);
            grid.Children.Add(splitter);

            border = new Border {
                BorderThickness = new Thickness(2, 0, 0, 0),
                BorderBrush = new SolidColorBrush(Color.FromRgb(250, 250, 250)),
            };
            Grid.SetColumn(border, 2);
            grid.Children.Add(border);
            
            DevelopersTableEditingView = new DevelopersTableEditingView();
            CategoriesTableEditingView = new CategoriesTableEditingView();
            EmployeesTableEditingView = new EmployeesTableEditingView();
            SoftwareTableEditingView = new SoftwareTableEditingView();
            InstallsTableEditingView = new InstallsTableEditingView();

            border.Child = InstallsTableEditingView;
        }

        #region SideBarView

        void SideBarView_SelectionChanged() {
            switch (sideBarView.SelectedButton) {
                case SideBarButton.Software:
                    border.Child = SoftwareTableEditingView;
                    break;
                case SideBarButton.Developers:
                    border.Child = DevelopersTableEditingView;
                    break;
                case SideBarButton.Categories:
                    border.Child = CategoriesTableEditingView;
                    break;
                case SideBarButton.Employees:
                    border.Child = EmployeesTableEditingView;
                    break;
                case SideBarButton.Installs:
                    border.Child = InstallsTableEditingView;
                    break;
                case SideBarButton.None:
                    border.Child = null;
                    break;
            }
        }

        #endregion
    }
}