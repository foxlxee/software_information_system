using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DataBaseInformationSystem {
    internal partial class SoftwareEditingWindow : Window {

        public SoftwareInfo SoftwareInfo { get; private set; }

        ObservableCollection<DeveloperInfo> developers;
        ObservableCollection<CategoryInfo> categories;

        public SoftwareEditingWindow(SoftwareInfo softwareInfo = null) {
            InitializeComponent();

            SoftwareInfo = softwareInfo;

            doneButton.Click += Button_Click;

            developers = MainWindow.Window.DevelopersTableEditingView.Collection();
            categories = MainWindow.Window.CategoriesTableEditingView.Collection();

            developerComboBox.ItemsSource = developers;
            categotyComboBox.ItemsSource = categories;

            if (softwareInfo != null) {
                nameTextBox.Text = softwareInfo.Name;

                for (int i = 0; i < developers.Count; i++) {
                    if (developers[i].Name.Equals(softwareInfo.Developer)) {
                        developerComboBox.SelectedIndex = i;
                        break;
                    }
                }

                for (int i = 0; i < categories.Count; i++) {
                    if (categories[i].Name.Equals(softwareInfo.Category)) {
                        categotyComboBox.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        void Button_Click(object sender, RoutedEventArgs e) {
            string text = nameTextBox.Text.Trim();

            if (text.Equals(string.Empty)) {
                MessageBox.Show("Введите название");
                nameTextBox.Text = null;
                nameTextBox.Focus();
                return;
            }

            if (developerComboBox.SelectedIndex == -1) {
                MessageBox.Show("Выберите разработчика");
                return;
            }

            if (categotyComboBox.SelectedIndex == -1) {
                MessageBox.Show("Выберите категорию");
                return;
            }

            DeveloperInfo developerInfo = developerComboBox.SelectedItem as DeveloperInfo;
            CategoryInfo categoryInfo = categotyComboBox.SelectedItem as CategoryInfo;

            if (MainWindow.Window.SoftwareTableEditingView.Collection().Where(
                n => n.Name.Equals(text) && n.Developer.Equals(developerInfo.Name) &&
                n.Category.Equals(categoryInfo.Name)).ToArray().Length > 0) {
                if (SoftwareInfo == null) {
                    MessageBox.Show("Это ПО уже есть в таблице");
                } else {
                    SoftwareInfo = null;
                }
                Close();
                return;
            }

            if (SoftwareInfo != null) {
                SoftwareInfo.Name = nameTextBox.Text;
                SoftwareInfo.Developer = developerInfo.Name;
                SoftwareInfo.Category = categoryInfo.Name;

                DataBaseManager.Instance.UpdateSoftware(
                    SoftwareInfo.Id,
                    SoftwareInfo.Name,

                    MainWindow.Window.DevelopersTableEditingView.Collection().Where(
                        n => n.Name.Equals(SoftwareInfo.Developer)).Select(
                        n => n.Id).ToArray()[0],

                    MainWindow.Window.CategoriesTableEditingView.Collection().Where(
                        n => n.Name.Equals(SoftwareInfo.Category)).Select(
                        n => n.Id).ToArray()[0]);
                Close();
                return;
            }

            SoftwareInfo = new SoftwareInfo {
                Id = DataBaseManager.Instance.InsertSoftware(text, developerInfo.Id, categoryInfo.Id),
                Name = nameTextBox.Text,
                Developer = developerInfo.Name,
                Category = categoryInfo.Name
            };
            Close();
        }

        protected override void OnKeyDown(KeyEventArgs e) {
            base.OnKeyDown(e);

            switch (e.Key) {
                case Key.Escape:
                    SoftwareInfo = null;
                    Close();
                    return;
                case Key.Enter:
                    Button_Click(null, null);
                    return;
            }
        }
    }
}