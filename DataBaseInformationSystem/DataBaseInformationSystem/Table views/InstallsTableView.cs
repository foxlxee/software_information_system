using System;

namespace DataBaseInformationSystem {
    internal class InstallsTableView : TableView {
        public InstallsTableView() {
            initColumns(new Tuple<string, string>[] {
                new Tuple<string, string>("Сотрудинк", "EmployeeFullNameShort"),
                new Tuple<string, string>("Кабинеты", "Rooms"),
                new Tuple<string, string>("ПО", "SoftwareName"),
                new Tuple<string, string>("Версия", "Version"),
                new Tuple<string, string>("Разработчик", "SoftwareDeveloper"),
                new Tuple<string, string>("Категория", "SoftwareCategory"),
                new Tuple<string, string>("Дата установки", "Date"),
            });
        }
    }
}