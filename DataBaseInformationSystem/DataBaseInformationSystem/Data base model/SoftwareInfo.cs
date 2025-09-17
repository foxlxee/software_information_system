namespace DataBaseInformationSystem {
    internal class SoftwareInfo : RowInfo {
        int id;
        string name;
        string developer;
        string category;

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
        public string Developer {
            get {
                return developer;
            }
            set {
                developer = value;
                OnPropertyChanged(nameof(Developer));
            }
        }
        public string Category {
            get {
                return category;
            }
            set {
                category = value;
                OnPropertyChanged(nameof(Category));
            }
        }

        public override string ToString() {
            return name;
        }
    }
}