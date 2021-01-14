using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MyDar.Areas.Admin.ViewModels
{
    public class CreateProfileViewModel
    {
        [Required]
        public string AboutUs { get; set; }
        
        [Display(Name ="About Image")]
        public IFormFile File { get; set; }
        [Required]
        public string Vission { get; set; }
        [Required]
        public string Mission { get; set; }
        [Required]
        public string Plan { get; set; }
        [Required]
        public string Care { get; set; }
        [Required]
        public string Address1 { get; set; }
        [Required]
        public string Address2 { get; set; }
        [Required]
        public string Address3 { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        [Required]
        public int ProjectsNum { get; set; }
        [Required]
        public int ClientNum { get; set; }
        [Required]
        public int WorkerNum { get; set; }
        [Required]
        public int HoursNum { get; set; }
    }
    public class EditProfileViewModel:CreateProfileViewModel
    {
        public int ID { get; set; }
        public string Image { get; set; }
    }
}
