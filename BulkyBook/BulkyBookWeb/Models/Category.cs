using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBookWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayName("Dsiaply Order")]
        [Range(1,20,ErrorMessage ="Qty must be within 1 to 50")]
        public int DisplayOrder { get; set; }
        public DateTime CreatedDateTime { get; set; }

    }
}
