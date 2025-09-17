using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DataBaseInformationSystem {
    internal class TextEditingWindow : Window {

        public string Text { get; private set; }

        TextBox textBox;
        Button button;

        public TextEditingWindow(string initialText = null) {
            WindowStyle = WindowStyle.ToolWindow;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ResizeMode = ResizeMode.NoResize;
            Width = 300;
            Height = 180;

            Text = initialText;

            DockPanel dp = new DockPanel();
            Content = dp;

            textBox = new TextBox {
                FontSize = 14,
                MaxLength = 255
            };
            button = new Button {
                Content = "Готово"
            };
            DockPanel.SetDock(button, Dock.Bottom);

            dp.Children.Add(button);
            dp.Children.Add(textBox);

            Loaded += TextEditingWindow_Loaded;
            button.Click += Button_Click;
        }

        void TextEditingWindow_Loaded(object sender, RoutedEventArgs e) {
            textBox.Focus();

            if (Text != null) {
                textBox.Text = Text;
                textBox.SelectAll();
            }
        }

        void Button_Click(object sender, RoutedEventArgs e) {
            Text = textBox.Text.Trim();

            if (Text.Equals(string.Empty)) {
                MessageBox.Show("Введите название");
                textBox.Text = null;
                textBox.Focus();
                return;
            }

            Close();
        }

        protected override void OnKeyDown(KeyEventArgs e) {
            base.OnKeyDown(e);

            switch (e.Key) {
                case Key.Escape:
                    Close();
                    return;
                case Key.Enter:
                    Button_Click(null, null);
                    return;
            }
        }
    }
}