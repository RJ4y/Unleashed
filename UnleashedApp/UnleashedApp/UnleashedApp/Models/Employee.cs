using Newtonsoft.Json;
using System;

namespace UnleashedApp.Models
{
    public class Employee
    {
        public Employee(int id, string firstName, string lastName, string function, int habitatId, DateTime startDate,
            DateTime? endDate, bool visibleSite,
            string pictureUrl, string motivation, string expectations, string needToKnow, DateTime dateOfBirth,
            char gender, string email)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Function = function;
            HabitatId = habitatId;
            StartDate = startDate;
            EndDate = endDate;
            VisibleSite = visibleSite;
            PictureUrl = pictureUrl;
            Motivation = motivation;
            Expectations = expectations;
            NeedToKnow = needToKnow;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Email = email;
        }

        public Employee() { }

        [JsonProperty(PropertyName = "id")] public int Id { get; set; }

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        public string FullName => FirstName + " " + LastName;

        [JsonProperty(PropertyName = "function")]
        public string Function { get; set; }

        [JsonProperty(PropertyName = "habitat")]
        public int HabitatId { get; set; }

        [JsonProperty(PropertyName = "start_date")]
        public DateTime StartDate { get; set; }

        [JsonProperty(PropertyName = "end_date")]
        public DateTime? EndDate { get; set; }

        [JsonProperty(PropertyName = "visible_site")]
        public bool VisibleSite { get; set; }

        [JsonProperty(PropertyName = "picture_url")]
        public string PictureUrl { get; set; }

        [JsonProperty(PropertyName = "motivation")]
        public string Motivation { get; set; }

        [JsonProperty(PropertyName = "expectations")]
        public string Expectations { get; set; }

        [JsonProperty(PropertyName = "need_to_know")]
        public string NeedToKnow { get; set; }

        [JsonProperty(PropertyName = "date_of_birth")]
        public DateTime DateOfBirth { get; set; }

        [JsonProperty(PropertyName = "gender")]
        public char Gender { get; set; }

        [JsonProperty(PropertyName = "email")] public string Email { get; set; }

        public override string ToString()
        {
            return FullName;
        }
    }
}