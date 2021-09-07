using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentInformation.Models
{
    public class Departments
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required")]
        public string DpName { get; set; }
    }
}
