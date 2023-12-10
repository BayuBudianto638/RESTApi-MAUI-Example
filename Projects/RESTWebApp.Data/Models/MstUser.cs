using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTWebApp.Data.Models
{
    [Table("MstUser", Schema = "dbo")]
    public class MstUser: TableData
    {
        [Required]
        [Column(TypeName = "Nvarchar(100)")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Column(TypeName = "Nvarchar(100)")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string Email { get; set; }
    }
}
