using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyDar.Areas.Admin.ViewModels
{
    public class CreatePackagesViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Description { get; set; }
    }
    public class EditPackagesViewModel: CreatePackagesViewModel
    {
        public int Id { get; set; }
    }
}
