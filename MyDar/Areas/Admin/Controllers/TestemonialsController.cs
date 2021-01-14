
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDar.Areas.Admin.Models;
using MyDar.Areas.Admin.ViewModels;
using MyDar.Areas.Admin.Models.Repositories;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace MyDar.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class TestemonialsController : Controller
    {
        private readonly IApplicationRepository<Testemonials> repository;
        private readonly IHostingEnvironment hosting;

        public TestemonialsController(IApplicationRepository<Testemonials> repository, IHostingEnvironment hosting)
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
            var test = repository.Find(id);
            if(test==null)
            {
                return NotFound();
            }
            return View(test);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateTestemonialsViewModel model)
        {
            try
            {
                string FileName = UploadFile(model.File) ?? string.Empty;
                if(ModelState.IsValid)
                {
                    var test = new Testemonials
                    {
                        Name = model.Name,
                        Job=model.Job,
                        Image=FileName,
                        Description=model.Description
                    };
                    repository.Add(test);
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
            var test = repository.Find(id);
            if(test==null)
            {
                return NotFound();
            }
            var model = new EditTestemonialsViewModel
            {
                Id = test.ID,
                Name = test.Name,
                Job = test.Job,
                Description = test.Description,
                ImageUrl = test.Image
            };
            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,EditTestemonialsViewModel model)
        {
            try
            {
                string FileName = UploadFile(model.File, model.ImageUrl);
                if(ModelState.IsValid)
                {
                    var test = new Testemonials
                    {
                        Name=model.Name,
                        Job=model.Job,
                        Image=FileName,
                        Description=model.Description
                    };
                    repository.Update(model.Id, test);
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
            var test = repository.Find(id);
            if(test==null)
            {
                return NotFound();
            }
            return View(test);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
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
                string uploads = Path.Combine(hosting.WebRootPath, "images/testemonials");
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
                string uploads = Path.Combine(hosting.WebRootPath, "images/testemonials");
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
