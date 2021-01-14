using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDar.Areas.Admin.Models;
using MyDar.Areas.Admin.Models.Repositories;
using MyDar.ViewModels;
namespace MyDar.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApplicationRepository<Slider> SliderRepository;
        private readonly IApplicationRepository<Services> ServicesRepository;
        private readonly IApplicationRepository<Videos> VideosRepository;
        private readonly IApplicationRepository<Features> FeatureRepository;
        private readonly IApplicationRepository<Profile> ProfileRepository;

        public HomeController(IApplicationRepository<Slider> SliderRepository, IApplicationRepository<Services> ServicesRepository,
            IApplicationRepository<Videos> VideosRepository, IApplicationRepository<Features> FeatureRepository,
            IApplicationRepository<Profile> ProfileRepository)
        {
            this.SliderRepository = SliderRepository;
            this.ServicesRepository = ServicesRepository;
            this.VideosRepository = VideosRepository;
            this.FeatureRepository = FeatureRepository;
            this.ProfileRepository = ProfileRepository;

        }
        // GET: HomeController
        public ActionResult Index()
        {
            var model = new IndexViewModel
            {
                Profiles=ProfileRepository.List().ToList(),
                Sliders = SliderRepository.List().ToList(),
                Videos = VideosRepository.List().ToList(),
                Features = FeatureRepository.List().ToList(),
                Services=ServicesRepository.List().ToList()
            };

            return View(model);
        }
    }
}
