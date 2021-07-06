using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewClient.ViewModels
{
    public class RegisterVM
    {
        public enum GenderType
        {
            Pria = 1,
            Wanita = 2
        }

        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Salary { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderType Gender { get; set; }
        public string Password { get; set; }
        public string Degree { get; set; }
        public string GPA { get; set; }
        public int EducationID { get; set; }
        public int UniversityID { get; set; }
    }
}
