using System;

namespace UnleashedApp.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public String First_Name { get; set; }
        public String Last_Name { get; set; }
        public String Function { get; set; }
        public int Habitat_Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
