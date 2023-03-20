using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBookWeb.Models
{
    public class Category
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CraetedDateTime { get; set; }

    }
}
