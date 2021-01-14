using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MyDar.Areas.Admin.Models
{
    public class Profile
    {
        public int ID { get; set; }
        public string AboutUs { get; set; }
        public string Image { get; set; }
        public string Vission { get; set; }
        public string Mission { get; set; }
        public string Plan { get; set; }
        public string Care { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int ProjectsNum { get; set; }
        public int ClientNum { get; set; }
        public int WorkerNum { get; set; }
        public int HoursNum { get; set; }


    }
}
