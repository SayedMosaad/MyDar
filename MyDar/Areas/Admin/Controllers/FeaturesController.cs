using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDar.Areas.Admin.Models;
using MyDar.Areas.Admin.Models.Repositories;
using MyDar.Areas.Admin.ViewModels;

namespace MyDar.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize]
    public class FeaturesController : Controller
    {
        private readonly IApplicationRepository<Features> repository;
        private readonly IHostingEnvironment hosting;

        public FeaturesController(IApplicationRepository<Features> repository, IHostingEnvironment hosting)
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
            var feature = repository.Find(id);
            return View(feature);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: FeaturesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateFeatureViewModel model)
        {
            try
            {
                string FileName = UploadFile(model.File) ?? string.Empty;
                if (ModelState.IsValid)
                {
                    Features feature = new Features
                    {
                        Title = model.Title,
                        Description = model.Description,
                        Image = FileName
                    };
                    repository.Add(feature);
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
            var feature = repository.Find(id);
            if(feature==null)
            {
                return NotFound();
            }
            var model = new EditFeatureViewModel
            {
                Id = feature.ID,
                Title = feature.Title,
                Description = feature.Description,
                Image = feature.Image
            };
            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditFeatureViewModel model)
        {
            try
            {
                string FileName = UploadFile(model.File, model.Image);
                if(ModelState.IsValid)
                {
                    var feature = new Features
                    {
                        Title = model.Title,
                        Description = model.Description,
                        Image = FileName
                    };
                    repository.Update(model.Id, feature);
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
            var feature = repository.Find(id);
            return View(feature);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                repository.Delete(id);
                return RedirectToAction("index");
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
                string uploads = Path.Combine(hosting.WebRootPath, "images/features");
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
                string uploads = Path.Combine(hosting.WebRootPath, "images/features");
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
