using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace MyDar.Areas.Admin.ViewModels
{
    public class CreateTeamViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public IFormFile File { get; set; }

        [Required]
        public string Job { get; set; }

        [Required]
        [StringLength(120,MinimumLength =50)]
        public string Bio { get; set; }
       
        [DataType(DataType.Url)]
        public string Facebook { get; set; }

        [DataType(DataType.Url)]
        public string Twitter { get; set; }

        [DataType(DataType.Url)]
        public string Instagram { get; set; }

        [DataType(DataType.Url)]
        public string Linkedin { get; set; }
    }
    public class EditTeamViewModel: CreateTeamViewModel
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
    }
}
