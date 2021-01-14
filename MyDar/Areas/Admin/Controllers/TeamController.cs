using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using MyDar.Areas.Admin.Models;
using MyDar.Areas.Admin.Models.Repositories;
using MyDar.Areas.Admin.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace MyDar.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class TeamController : Controller
    {
        private readonly IApplicationRepository<Team> repository;
        private readonly IHostingEnvironment hosting;

        public TeamController(IApplicationRepository<Team> repository, IHostingEnvironment hosting)
        {
            this.repository = repository;
            this.hosting = hosting;
        }
        
        public ActionResult Index()
        {

            return View(repository.List());
        }


        public ActionResult Details(int id)
        {
            var team = repository.Find(id);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateTeamViewModel model)
        {
            try
            {
                string FileName = UploadFile(model.File) ?? string.Empty;
                if(ModelState.IsValid)
                {
                    var team = new Team
                    {
                        Name=model.Name,
                        Image=FileName,
                        Job=model.Job,
                        Bio=model.Bio,
                        Facebook=model.Facebook,
                        Instagram=model.Instagram,
                        Twitter=model.Twitter,
                        Linkedin=model.Linkedin
                    };
                    repository.Add(team);
                    return RedirectToAction("index");
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var team = repository.Find(id);
            if(team==null)
            {
                return NotFound();
            }
            var model = new EditTeamViewModel
            {
                Id=team.ID,
                Name=team.Name,
                ImageUrl=team.Image,
                Job=team.Job,
                Bio=team.Bio,
                Facebook=team.Facebook,
                Twitter=team.Twitter,
                Instagram=team.Instagram,
                Linkedin=team.Linkedin
            };

            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditTeamViewModel model)
        {
            try
            {
                string FileName = UploadFile(model.File, model.ImageUrl);
                if (ModelState.IsValid)
                {
                    var team = new Team
                    {
                        Name = model.Name,
                        Image = FileName,
                        Job = model.Job,
                        Bio = model.Bio,
                        Facebook = model.Facebook,
                        Twitter = model.Twitter,
                        Instagram = model.Instagram,
                        Linkedin = model.Linkedin
                    };
                    repository.Update(model.Id, team);
                    return RedirectToAction("index");
                }
                return View(model);
                
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var team = repository.Find(id);
            if(team==null)
            {
                return NotFound();
            }
            return View(team);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                repository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        string UploadFile(IFormFile file)
        {
            //check if there is a file or not coming from the view
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "images/team");
                string filename = file.FileName;
                string fullpath = Path.Combine(uploads, filename);
                file.CopyTo(new FileStream(fullpath, FileMode.Create));
                return file.FileName;
            }
            return null;
        }

        string UploadFile(IFormFile file, string ImageUrl)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "images/team");
                string FileName = file.FileName;
                string newpath = Path.Combine(uploads, FileName);
                string OldFileName = ImageUrl;
                string oldpath = Path.Combine(uploads, OldFileName);
                if (newpath != oldpath)
                {
                    System.IO.File.Delete(oldpath);
                    file.CopyTo(new FileStream(newpath, FileMode.Create));
                }
                return file.FileName;
            }
            return ImageUrl;
        }
    }
}
