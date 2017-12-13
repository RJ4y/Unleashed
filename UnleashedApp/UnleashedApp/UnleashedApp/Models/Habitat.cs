using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnleashedApp.Models
{
    public class Habitat
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
