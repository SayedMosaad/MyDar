using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDar.Areas.Admin.Models
{
    public class Team
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Job { get; set; }
        public string Bio { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Linkedin { get; set; }
    }
}
