using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_t_Role")]
    public class Role
    {
        [Key]
        public string RoleID { get; set; }
        public string RoleName { get; set; }
        //public Employee NIK { get; set; }
        //[JsonIgnore]
        //public virtual Employee Employee { get; set; }
        //[JsonIgnore]
        //public virtual Profiling Profiling { get; set; }
        [JsonIgnore]
        public virtual ICollection<AccountRole> AccountRoles { get; set; }

    }
}
