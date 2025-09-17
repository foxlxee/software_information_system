using System;

namespace DataBaseInformationSystem {
    internal class SoftwareTableView : TableView {
        public SoftwareTableView() {
            initColumns(new Tuple<string, string>[] {
                new Tuple<string, string>("Название", "Name"),
                new Tuple<string, string>("Разработчик", "Developer"),
                new Tuple<string, string>("Категория", "Category")
            });
        }
    }
}