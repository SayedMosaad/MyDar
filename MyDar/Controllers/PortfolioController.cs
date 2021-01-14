using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyDar.Areas.Admin.Models.Repositories;
using MyDar.Areas.Admin.Models;
using MyDar.ViewModels;

namespace MyDar.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly IApplicationRepository<Categories> categoriesRepository;
        private readonly IApplicationRepository<Projects> projectsRepository;
        private readonly IApplicationRepository<Photos> photosRepository;
        private readonly IApplicationRepository<Profile> profileRepository;

        public PortfolioController(IApplicationRepository<Categories> categoriesRepository,
                                    IApplicationRepository<Projects> projectsRepository,
                                    IApplicationRepository<Photos> photosRepository,
                                    IApplicationRepository<Profile> profileRepository)
        {
            this.categoriesRepository = categoriesRepository;
            this.projectsRepository = projectsRepository;
            this.photosRepository = photosRepository;
            this.profileRepository = profileRepository;
        }
        public IActionResult Index()
        {
            var model = new IndexViewModel
            {
                Profiles = profileRepository.List().ToList(),
                Categories = categoriesRepository.List().ToList(),
                Projects=projectsRepository.List().ToList(),
                Photos=photosRepository.List().ToList()

            };
            return View(model);
        }
        public IActionResult PortfolioDetails(int id)
        {
            var project = projectsRepository.Find(id);
            if(project==null)
            {
                return NotFound();
            }
            var model = new IndexViewModel
            {
                Project = project,
                Profiles = profileRepository.List().ToList()
            };
            return View(model);
        }
    }
}
