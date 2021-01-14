using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyDar.Areas.Admin.ViewModels
{
    public class CreateTestemonialsViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Job { get; set; }
       
        [Display(Name ="Testemonial Image")]
        public IFormFile File { get; set; }

        [Required]
        [Display(Name ="Openion")]
        public string Description { get; set; }
    }
    public class EditTestemonialsViewModel:CreateTestemonialsViewModel
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
    }
}
