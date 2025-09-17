using System;

namespace DataBaseInformationSystem {
    internal class DevelopersTableView : TableView {
        public DevelopersTableView() {
            initColumns(new Tuple<string, string>[] {
                new Tuple<string, string>("Название", "Name")
            });
        }
    }
}