using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CargoManagement.Admin.Models
{
    public class AdminViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Provide User Name")]
        [Display(Name="User Name")]
        public string UserName { get; set; }

        public string? Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [DefaultValue("Admin@123")]

        public string Password { get; set; }
    }
}
