using System;
using System.Collections.Generic;

namespace DockerSample.Models
{
    public partial class Department
    {
        public Department()
        {
            Employee = new HashSet<Employee>();
        }

        public int Deptid { get; set; }
        public string Deptname { get; set; }
        public string Location { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
