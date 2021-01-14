using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyDar.Areas.Admin.ViewModels
{
    public class CreateFeatureViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        [StringLength(120, MinimumLength = 50)]
        public string Description { get; set; }
        public IFormFile File { get; set; }
    }
    public class EditFeatureViewModel:CreateFeatureViewModel
    {
        public int Id { get; set; }
        public string Image { get; set; }
    }


}
