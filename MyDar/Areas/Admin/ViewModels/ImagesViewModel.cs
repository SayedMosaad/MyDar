using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyDar.Areas.Admin.Models;
using System.ComponentModel.DataAnnotations;

namespace MyDar.Areas.Admin.ViewModels
{
    public class CreateImagesViewModel
    {
        public IFormFile File { get; set; }
        [Display(Name ="Project")]
        public int ProjectId { get; set; }
        public List<Projects> Projects { get; set; }
    }
    public class EditImagesViewModel:CreateImagesViewModel
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
    }
}
