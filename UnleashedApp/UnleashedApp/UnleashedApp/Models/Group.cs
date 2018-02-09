using System.Collections.ObjectModel;

namespace UnleashedApp.Models
{
    public class Group : ObservableCollection<Employee>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Group(Habitat habitat)
        {
            Id = habitat.Id;
            Name = habitat.Name;
        }

        public Group(Squad squad)
        {
            Id = squad.Id;
            Name = squad.Name;
        }

        public Group(Group group)
        {
            Id = group.Id;
            Name = group.Name;
        }

        public Group() { }
    }
}