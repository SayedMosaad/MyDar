using Microsoft.AspNetCore.Http;
using MyDar.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyDar.Areas.Admin.ViewModels
{
    
    public class CreateProjectsViewModel
    {
        [Required]
        public string Name { get; set; }

        public IFormFile File { get; set; }

        [Required]
        [Display(Name ="Project Description")]
        public string Description { get; set; }

        [Required]
        public string Client { get; set; }

        [Required]
        [Display(Name = "Project Date")]
        public DateTime ProjectDate { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public List<Categories> Categories { get; set; }
    }

    public class EditProjectsViewModel:CreateProjectsViewModel
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }
        
    }
}
