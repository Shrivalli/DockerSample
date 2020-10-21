using System;
using System.Collections.Generic;

namespace DockerSample.Models
{
    public partial class Employee
    {
        public int Eid { get; set; }
        public string Ename { get; set; }
        public double? Salary { get; set; }
        public DateTime? Doj { get; set; }
        public string Designation { get; set; }
        public int? Deptid { get; set; }

        public virtual Department Dept { get; set; }
    }
}
