namespace DataBaseInformationSystem {
    internal class EmployeeInfo : RowInfo {
        int id;
        string fullName;

        public int Id {
            get {
                return id;
            }
            set {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public string FullName {
            get {
                return fullName;
            }
            set {
                fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }

        public override string ToString() {
            return FullName;
        }
    }
}