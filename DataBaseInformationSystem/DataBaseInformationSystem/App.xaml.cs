using System.Diagnostics;
using System.Threading;
using System.Windows;

namespace DataBaseInformationSystem {
    internal partial class App : Application {

        static Mutex mutex;

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);
            
            bool createdNew;
            mutex = new Mutex(true, "b39860ac3b64428c91866a3b2a537ca2", out createdNew);
            if (!createdNew) {
                MessageBox.Show("Приложение уже запущено");
                Process.GetCurrentProcess().Kill();
            }

            if (!DataBaseManager.Instance.Connect()) {
                MessageBox.Show("Не удалось подключиться", string.Empty, MessageBoxButton.OK, MessageBoxImage.Error);
                Process.GetCurrentProcess().Kill();
            }

            ShutdownMode = ShutdownMode.OnMainWindowClose;
            MainWindow = new MainWindow();
            MainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e) {
            base.OnExit(e);

            mutex.Close();
        }
    }
}