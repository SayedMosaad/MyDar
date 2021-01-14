using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MyDar.Areas.Admin.ViewModels
{
    public class CreateBlogsViewModel
    {
        [Required]
        [Display(Name ="Blog Name")]
        public string Title { get; set; }
        [Required]
        [Display(Name ="Blog Photo")]
        public IFormFile File { get; set; }
        [Required]
        [StringLength(150,MinimumLength =50)]
        public string Description { get; set; }
        [Required]
        public string Body { get; set; }

    }
    public class EditBlogsViewModel:CreateBlogsViewModel
    {
        public int Id { get; set; }
        [Required]
        public string ImageUrl { get; set; }
    }
}
