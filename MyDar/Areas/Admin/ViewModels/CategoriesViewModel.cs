using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyDar.Areas.Admin.ViewModels
{
    public class CreateCategoriesViewModel
    {
        [Required]
        [Display(Name ="Category Name")]
        public string Name { get; set; }
    }
    public class EditCategoriesViewModel:CreateCategoriesViewModel
    {
        public int Id { get; set; }
    }
}
