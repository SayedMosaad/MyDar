using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyDar.ViewModels;
using MyDar.Areas.Admin.Models.Repositories;
using MyDar.Areas.Admin.Models;

namespace MyDar.Controllers
{
    public class TeamController : Controller
    {
        private readonly IApplicationRepository<Team> teamRepository;
        private readonly IApplicationRepository<Profile> profileRepository;

        public TeamController(IApplicationRepository<Team> TeamRepository,IApplicationRepository<Profile> ProfileRepository)
        {
            teamRepository = TeamRepository;
            profileRepository = ProfileRepository;
        }
        public IActionResult Index()
        {
            var model = new IndexViewModel
            {
                Profiles=profileRepository.List().ToList(),
                Team=teamRepository.List().ToList()
            };
            return View(model);
        }
    }
}
