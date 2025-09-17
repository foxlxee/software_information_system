namespace DataBaseInformationSystem {
    internal class CategoryInfo : RowInfo {
        int id;
        string name;

        public int Id {
            get {
                return id;
            }
            set {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public string Name {
            get {
                return name;
            }
            set {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public override string ToString() {
            return Name;
        }
    }
}