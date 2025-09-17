using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace DataBaseInformationSystem {
    internal class SideBarView : UserControl {

        public SideBarButton SelectedButton;
        public event Action SelectionChanged;

        ToggleButton installsButton;
        ToggleButton softwareButton;
        ToggleButton developersButton;
        ToggleButton categoriesButton;
        ToggleButton employeesButton;

        ToggleButton[] buttons;

        public SideBarView(SideBarButton selectedButton = SideBarButton.None) {
            Background = new SolidColorBrush(Color.FromRgb(250, 250, 250));

            Grid grid = new Grid();
            Content = grid;

            installsButton = new ToggleButton {
                Content = "Установки",
                Tag = SideBarButton.Installs
            };
            softwareButton = new ToggleButton {
                Content = "ПО",
                Tag = SideBarButton.Software
            };
            developersButton = new ToggleButton {
                Content = "Разработчики",
                Tag = SideBarButton.Developers
            };
            categoriesButton = new ToggleButton {
                Content = "Категории",
                Tag = SideBarButton.Categories
            };
            employeesButton = new ToggleButton {
                Content = "Сотрудники",
                Tag = SideBarButton.Employees
            };

            buttons = new ToggleButton[] {
                installsButton,
                softwareButton,
                developersButton,
                categoriesButton,
                employeesButton
            };

            SelectedButton = selectedButton;

            switch (selectedButton) {
                case SideBarButton.Installs:
                    installsButton.IsChecked = true;
                    break;
                case SideBarButton.Software:
                    softwareButton.IsChecked = true;
                    break;
                case SideBarButton.Developers:
                    developersButton.IsChecked = true;
                    break;
                case SideBarButton.Categories:
                    categoriesButton.IsChecked = true;
                    break;
                case SideBarButton.Employees:
                    employeesButton.IsChecked = true;
                    break;
            }

            foreach (var item in buttons) {
                item.Checked += Button_Checked;
                item.Unchecked += Item_Unchecked;
            }

            StackPanel spTop = new StackPanel {
                VerticalAlignment = VerticalAlignment.Top
            };
            spTop.Children.Add(new Label {
                Content = "Операционные",
                HorizontalAlignment = HorizontalAlignment.Center
            });
            spTop.Children.Add(new Separator());
            spTop.Children.Add(installsButton);

            StackPanel spBottom = new StackPanel {
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(0, 0, 0, 8)
            };
            spBottom.Children.Add(new Label {
                Content = "Справочники",
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 100, 0, 0)
            });
            spBottom.Children.Add(new Separator());
            spBottom.Children.Add(softwareButton);
            spBottom.Children.Add(developersButton);
            spBottom.Children.Add(categoriesButton);
            spBottom.Children.Add(employeesButton);

            grid.Children.Add(spTop);
            grid.Children.Add(spBottom);
        }

        void Item_Unchecked(object sender, RoutedEventArgs e) {
            SelectedButton = SideBarButton.None;
            SelectionChanged?.Invoke();
        }

        void Button_Checked(object sender, RoutedEventArgs e) {
            foreach (var item in buttons) item.Unchecked -= Item_Unchecked;

            SelectedButton = (SideBarButton)(sender as ToggleButton).Tag;

            foreach (var button in buttons) {
                if ((SideBarButton)(button as ToggleButton).Tag != SelectedButton) {
                    if (button.IsChecked.Value) {
                        button.IsChecked = false;
                        break;
                    }
                }
            }

            foreach (var item in buttons) item.Unchecked += Item_Unchecked;

            SelectionChanged?.Invoke();
        }
    }
}