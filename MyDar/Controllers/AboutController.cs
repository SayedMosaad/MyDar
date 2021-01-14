using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyDar.Areas.Admin.Models;
using MyDar.Areas.Admin.Models.Repositories;
using MyDar.ViewModels;

namespace MyDar.Controllers
{
    
    public class AboutController : Controller
    {

        private readonly IApplicationRepository<Testemonials> TestemonialsRepository;
        private readonly IApplicationRepository<Profile> ProfileRepository;

        public AboutController(IApplicationRepository<Testemonials> TestemonialsRepository,
            IApplicationRepository<Profile> ProfileRepository)
        {

            this.TestemonialsRepository = TestemonialsRepository;
            this.ProfileRepository = ProfileRepository;

        }
        public IActionResult Index()
        {
            var model = new IndexViewModel
            {
                Profiles = ProfileRepository.List().ToList(),
                Testemonials = TestemonialsRepository.List().ToList()
            };
            return View(model);
        }
    }
}
