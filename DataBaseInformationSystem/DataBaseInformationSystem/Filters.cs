using System;

namespace DataBaseInformationSystem {
    internal class Filters {
        public EmployeeInfo[] Employees { get; set; }
        public SoftwareInfo[] Software { get; set; }
        public int[] Rooms { get; set; }
        public DateTime FromDateTime { get; set; }
        public DateTime ToDateTime { get; set; }
    }
}