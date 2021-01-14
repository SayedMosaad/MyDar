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
    public class ServicesController : Controller
    {
        private readonly IApplicationRepository<Profile> profileRepository;
        private readonly IApplicationRepository<Services> servicesRepository;
        private readonly IApplicationRepository<Videos> videoRepository;
        private readonly IApplicationRepository<Packages> packagesRepository;

        public ServicesController(IApplicationRepository<Profile> ProfileRepository,
                                    IApplicationRepository<Services> ServicesRepository,
                                    IApplicationRepository<Videos> VideoRepository,
                                    IApplicationRepository<Packages> PackagesRepository)
        {
            profileRepository = ProfileRepository;
            this.servicesRepository = ServicesRepository;
            videoRepository = VideoRepository;
            packagesRepository = PackagesRepository;
        }
        public IActionResult Index()
        {
            var model = new IndexViewModel
            {
                Profiles = profileRepository.List().ToList(),
                Services = servicesRepository.List().ToList(),
                Videos = videoRepository.List().ToList(),
                Packages = packagesRepository.List().ToList()
            };

            return View(model);
        }
    }
}
