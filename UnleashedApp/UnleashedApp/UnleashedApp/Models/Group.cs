using System;
using System.Collections.ObjectModel;

namespace UnleashedApp.Models
{
    public class Group : ObservableCollection<Employee>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
