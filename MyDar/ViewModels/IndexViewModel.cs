using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MyDar.Areas.Admin.Models;

namespace MyDar.ViewModels
{
    public class IndexViewModel
    {
        public List<Slider> Sliders { get; set; }
        public List<Profile> Profiles{ get; set; }
        public List<Services> Services { get; set; }
        public List<Videos> Videos { get; set; }
        public List<Features> Features { get; set; }
        public List<Testemonials> Testemonials { get; set; }
        public List<Packages> Packages { get; set; }
        public List<Categories> Categories { get; set; }
        public List<Projects> Projects { get; set; }
        public Projects Project { get; set; }
        public List<Photos> Photos { get; set; }
        public List<Team> Team { get; set; }
        public List<Blogs> Blogs { get; set; }
        public Blogs Blog { get; set; }

        [Required]
        [MinLength(4,ErrorMessage = "Please enter at least 4 chars")]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }

        [Required]
        [MinLength(8,ErrorMessage = "Please enter at least 8 chars of subject")]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }


    }
}
