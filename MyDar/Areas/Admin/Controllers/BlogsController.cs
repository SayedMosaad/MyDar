using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDar.Areas.Admin.Models;
using MyDar.Areas.Admin.Models.Repositories;
using MyDar.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace MyDar.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BlogsController : Controller
    {
        private readonly IApplicationRepository<Blogs> repository;
        private readonly IHostingEnvironment hosting;

        public BlogsController(IApplicationRepository<Blogs> repository, IHostingEnvironment hosting)
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
            var blog = repository.Find(id);
            if(blog==null)
            {
                return NotFound();
            }
            return View(blog);
        }

        
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateBlogsViewModel model)
        {
            try
            {
                string FileName = UploadFile(model.File) ?? string.Empty;
                if(ModelState.IsValid)
                {
                    var blog = new Blogs
                    {
                        Title = model.Title,
                        Image = FileName,
                        Description = model.Description,
                        Body = model.Body
                    };
                    repository.Add(blog);
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
            var blog = repository.Find(id);
            if(blog==null)
            {
                return NotFound();
            }
            var model = new EditBlogsViewModel
            {
                Id=blog.ID,
                Title=blog.Title,
                ImageUrl=blog.Image,
                Description=blog.Description,
                Body=blog.Body
            };
            return View(model);
        }

        [HttpPost]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditBlogsViewModel model)
        {
            try
            {
                string FileName = UploadFile(model.File, model.ImageUrl);
                if (ModelState.IsValid)
                {
                    var blog = new Blogs
                    {
                        Title = model.Title,
                        Image = FileName,
                        Description = model.Description,
                        Body = model.Body
                    };
                    repository.Update(model.Id,blog);
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
            var blog = repository.Find(id);
            if(blog==null)
            {
                return NotFound();
            }
            return View(blog);
        }

        // POST: BlogsController/Delete/5
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
                string uploads = Path.Combine(hosting.WebRootPath, "images/blogs");
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
                string uploads = Path.Combine(hosting.WebRootPath, "images/blogs");
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
