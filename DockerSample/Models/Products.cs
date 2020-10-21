using System;
using System.Collections.Generic;

namespace DockerSample.Models
{
    public partial class Products
    {
        public int Pid { get; set; }
        public string Pname { get; set; }
        public int Qty { get; set; }
        public int Price { get; set; }
        public DateTime Dop { get; set; }
    }
}
