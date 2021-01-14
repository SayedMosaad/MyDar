using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDar.Areas.Admin.Models
{
    public class Categories
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Projects> Projects { get; set; }
    }
}
