using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MyDar.Areas.Admin.ViewModels
{
    public class CreateServicesViewModel
    {
        
        [Required]
        public string Title { get; set; }
        [Required]
        [StringLength(120,MinimumLength =50)]
        public string Description { get; set; }
    }
    public class EditServicesViewModel : CreateServicesViewModel
    {
        public int Id { get; set; }
    }
}
