using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnleashedApp.Models
{
    public class Group : ObservableCollection<Employee>
    {
        public int Id { get; set; }
        public String Name { get; set; }
    }
}
