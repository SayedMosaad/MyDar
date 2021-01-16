using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyDar.ViewModels;
using MyDar.Areas.Admin.Models;
using MyDar.Areas.Admin.Models.Repositories;

namespace MyDar.Controllers
{
    public class ContactController : Controller
    {
        private readonly IApplicationRepository<Profile> profileRepository;
        private readonly IApplicationRepository<Request> requestRepository;

        public ContactController(IApplicationRepository<Profile> ProfileRepository, IApplicationRepository<Request> RequestRepository)
        {
            profileRepository = ProfileRepository;
            requestRepository = RequestRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var model = new IndexViewModel
            {
                Profiles = profileRepository.List().ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(IndexViewModel model)
        {
            if(ModelState.IsValid)
            {
                var request = new Request
                {
                    Name = model.Name,
                    Phone=model.Phone,
                    Email = model.Email,
                    Subject = model.Subject,
                    Message = model.Message
                };
                requestRepository.Add(request);
                model.Profiles = profileRepository.List().ToList();
                return RedirectToAction("index");
            }
            return View("Index");
            
        }

        public IActionResult Result()
        {
            var model = new IndexViewModel
            {
                Profiles = profileRepository.List().ToList()
            };
            return View(model);
        }


    }
}
