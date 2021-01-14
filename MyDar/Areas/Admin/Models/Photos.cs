using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDar.Areas.Admin.Models
{
    public class Photos
    {
        public int ID { get; set; }
        public string Image { get; set; }
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public Projects Project { get; set; }
    }
}
