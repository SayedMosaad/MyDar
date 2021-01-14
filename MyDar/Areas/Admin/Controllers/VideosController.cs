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
    public class VideosController : Controller
    {
        private readonly IApplicationRepository<Videos> repository;
        private readonly IHostingEnvironment hosting;

        public VideosController(IApplicationRepository<Videos> repository,IHostingEnvironment hosting)
        {
            this.repository = repository;
            this.hosting = hosting;
        }
        public ActionResult Index()
        {
            var video = repository.List();
            return View(video);
            //return View(repository.List());
        }

      

        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateVideosViewModel model)
        {
            try
            {
                //string FileName = UploadFile(model.File) ?? string.Empty;
                if(ModelState.IsValid)
                {
                    var video = new Videos
                    {
                        Video = model.File,
                        Title1 = model.Title1,
                        Title2 = model.Title2,
                        Description1 = model.Description1,
                        Description2 = model.Description2
                    };
                    repository.Add(video);
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
            var video = repository.Find(id);
            if(video==null)
            {
                return NotFound();
            }
            var model = new EditVideosViewModel
            {
                Id = video.ID,
                File = video.Video,
                Title1 = video.Title1,
                Title2 = video.Title2,
                Description1 = video.Description1,
                Description2 = video.Description2
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditVideosViewModel model)
        {
            try
            {
                //string FileName = UploadFile(model.File, model.VideoUrl);
                if(ModelState.IsValid)
                {
                    var video = new Videos
                    {
                        Video = model.File,
                        Title1 = model.Title1,
                        Title2 = model.Title2,
                        Description1 = model.Description1,
                        Description2 = model.Description2
                    };
                    repository.Update(model.Id, video);
                    return RedirectToAction("index");
                }
                return View(model);
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
                string uploads = Path.Combine(hosting.WebRootPath, "video");
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
                string uploads = Path.Combine(hosting.WebRootPath, "video");
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
