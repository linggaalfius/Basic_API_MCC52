using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_M_Employee")] //untuk memberi nama tabel sesuai keinginan kita
    public class Employee
    {
        public enum GenderType
        {
            Pria = 1,
            Wanita =2
        }

        [Key]
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Salary { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderType Gender { get; set; }

        [JsonIgnore]
        public virtual Account Account { get; set; }
    }


}
