using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyDar.Areas.Admin.ViewModels
{
    public class CreateSliderViewModel
    {
        [Required(ErrorMessage ="Title is Required")]
        [Display(Name ="Slider Title")]
        public string Title { get; set; }

        [Required(ErrorMessage ="Description is Required")]
        public string Description { get; set; }
    }

    public class EditSliderViewModel: CreateSliderViewModel
    {
        public int ID { get; set; }

    }
}
