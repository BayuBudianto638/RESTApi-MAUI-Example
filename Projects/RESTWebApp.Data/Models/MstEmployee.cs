using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTWebApp.Data.Models
{
    [Table("MstEmployee", Schema = "dbo")]
    public class MstEmployee: TableData
    {
        [Required]
        [Column(TypeName = "Nvarchar(10)")]
        [Display(Name = "Employee Code")]
        public string Code { get; set; }

        [Required]
        [Column(TypeName = "Nvarchar(100)")]
        [Display(Name = "Employee Name")]
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
