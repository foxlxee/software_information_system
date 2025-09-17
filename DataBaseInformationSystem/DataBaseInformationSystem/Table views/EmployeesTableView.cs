using System;

namespace DataBaseInformationSystem {
    internal class EmployeesTableView : TableView {
        public EmployeesTableView() {
            initColumns(new Tuple<string, string>[] {
                new Tuple<string, string>("ФИО", "FullName")
            });
        }
    }
}