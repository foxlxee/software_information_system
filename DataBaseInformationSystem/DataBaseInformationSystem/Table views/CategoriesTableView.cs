using System;

namespace DataBaseInformationSystem {
    internal class CategoriesTableView : TableView {
        public CategoriesTableView() {
            initColumns(new Tuple<string, string>[] {
                new Tuple<string, string>("Название", "Name")
            });
        }
    }
}