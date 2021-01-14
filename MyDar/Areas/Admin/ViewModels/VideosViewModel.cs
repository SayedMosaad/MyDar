using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MyDar.Areas.Admin.ViewModels
{
    public class CreateVideosViewModel
    {
        
        [Required]
        [Display(Name ="Video URL")]
        public string File { get; set; }

        [Required]
        public string Title1 { get; set; }

        [Required]
        [StringLength(200,MinimumLength =100)]
        public string Description1 { get; set; }

        [Required]
        public string Title2 { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 100)]
        public string Description2 { get; set; }
    }
    public class EditVideosViewModel:CreateVideosViewModel
    {
        public int Id { get; set; }
       
    }
}
