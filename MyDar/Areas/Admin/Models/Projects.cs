using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDar.Areas.Admin.Models
{
    public class Projects
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Client { get; set; }
        public DateTime ProjectDate { get; set; }
        public int CategoryId { get; set; }
        public Categories Category { get; set; }
        public ICollection<Photos> Photos { get; set; }

    }
}
