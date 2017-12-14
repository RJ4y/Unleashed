using System;
using System.Collections.Generic;

namespace UnleashedApp.Models
{
    public class Squad
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
