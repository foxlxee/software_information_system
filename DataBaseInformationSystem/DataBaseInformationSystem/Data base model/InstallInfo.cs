using System.Collections.Generic;

namespace DataBaseInformationSystem {
    internal class InstallInfo : RowInfo {
        int id;
        string softwareName;
        string softwareDeveloper;
        string softwareCategory;
        string version;
        string employeeFullName;
        string rooms;
        string date;

        public int Id {
            get {
                return id;
            }
            set {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public string SoftwareName {
            get {
                return softwareName;
            }
            set {
                softwareName = value;
                OnPropertyChanged(nameof(SoftwareName));
            }
        }
        public string SoftwareDeveloper {
            get {
                return softwareDeveloper;
            }
            set {
                softwareDeveloper = value;
                OnPropertyChanged(nameof(SoftwareDeveloper));
            }
        }
        public string SoftwareCategory {
            get {
                return softwareCategory;
            }
            set {
                softwareCategory = value;
                OnPropertyChanged(nameof(SoftwareCategory));
            }
        }
        public string EmployeeFullNameShort {
            get {
                string[] words = EmployeeFullName.Split(' ');
                if (words.Length != 3) return EmployeeFullName;
                return words[0] + ' ' + words[1][0] + '.' + words[2][0] + '.';
            }
        }
        public string EmployeeFullName {
            get {
                return employeeFullName;
            }
            set {
                employeeFullName = value;
                OnPropertyChanged(nameof(EmployeeFullName));
                OnPropertyChanged(nameof(EmployeeFullNameShort));
            }
        }
        public string Version {
            get {
                return version;
            }
            set {
                version = value;
                OnPropertyChanged(nameof(Version));
            }
        }
        public string Rooms {
            get {
                return rooms;
            }
            set {
                rooms = value;
                OnPropertyChanged(nameof(Rooms));
            }
        }
        public string Date {
            get {
                return date;
            }
            set {
                date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        public void Copy(InstallInfo other) {
            SoftwareName = other.SoftwareName;
            SoftwareDeveloper = other.SoftwareDeveloper;
            SoftwareCategory = other.SoftwareCategory;
            EmployeeFullName = other.EmployeeFullName;
            Version = other.Version;
            Rooms = other.Rooms;
            Date = other.Date;
        }

        public HashSet<int> GetRooms() {
            HashSet<int> result = new HashSet<int>();

            string[] rooms = Rooms.Split(',');

            foreach (string room in rooms) {
                result.Add(int.Parse(room));
            }

            return result;
        }

        public bool Compare(InstallInfo other) {
            if (other == null) return false;
            return
                SoftwareName.Equals(other.SoftwareName) &&
                SoftwareDeveloper.Equals(other.SoftwareDeveloper) &&
                SoftwareCategory.Equals(other.SoftwareCategory) &&
                EmployeeFullName.Equals(other.EmployeeFullName) &&
                Version.Equals(other.Version) &&
                GetRooms().Equals(other.GetRooms()) &&
                Date.Equals(other.Date);
        }
    }
}