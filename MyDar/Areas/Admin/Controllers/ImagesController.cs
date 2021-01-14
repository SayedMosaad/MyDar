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
    public class ImagesController : Controller
    {
        private readonly IimagesRepository imageRepository;
        private readonly IApplicationRepository<Projects> projectRepository;
        private readonly IHostingEnvironment hosting;

        public ImagesController(IimagesRepository imageRepository, IApplicationRepository<Projects> projectRepository, IHostingEnvironment hosting)
        {
            this.imageRepository = imageRepository;
            this.projectRepository = projectRepository;
            this.hosting = hosting;
        }

        public ActionResult Index()
        {
            var projects = projectRepository.List();
            return View(projects);
        }


        public ActionResult Details(int id)
        {
            return View();
        }


        public ActionResult Create()
        {

            var model = new CreateImagesViewModel
            {
                Projects = FillProjects()
            };
            return View(model);
        }
        


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateImagesViewModel model)
        {
            try
            {
                string FileName = UploadFile(model.File) ?? string.Empty;
                if(ModelState.IsValid)
                {
                    if(model.ProjectId==-1)
                    {
                        ModelState.AddModelError("", "please review the input fileds");
                        return View(GetAllProjects());
                    }
                    var projects = projectRepository.Find(model.ProjectId);
                    var photo = new Photos
                    {
                        Image=FileName,
                        Project=projects
                    };
                    imageRepository.Add(photo);
                    return RedirectToAction("index");
                }
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        List<Projects> FillProjects()
        {
            var projects = projectRepository.List().ToList();
            projects.Insert(0, new Projects { ID = -1, Name = "---- Please Select Project ----" });
            return projects;
        }
        CreateImagesViewModel GetAllProjects()
        {
            var model = new CreateImagesViewModel
            {
                Projects = FillProjects()
            };
            return model;
        }

        public ActionResult GetImages(int id)
        {
            List<Photos> photos = imageRepository.GetImages(id).ToList();
            return View(photos);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var image = imageRepository.Find(id);
            if(image==null)
            {
                return NotFound();
            }

            var projectid = image.Project == null ? image.Project.ID = 0 : image.Project.ID;
            var model = new EditImagesViewModel
            {
                Id=image.ID,
                ImageUrl=image.Image,
                ProjectId=projectid,
                Projects=FillProjects()
            };
            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditImagesViewModel model)
        {
            try
            {
                string FileName = UploadFile(model.File, model.ImageUrl);
                if(ModelState.IsValid)
                {
                    var Projects = projectRepository.Find(model.ProjectId);
                    if(Projects==null)
                    {
                        ModelState.AddModelError("", "please review the input fileds");
                        return View(GetAllProjects());
                    }
                    var photo = new Photos
                    {
                        Image = FileName,
                        Project = Projects
                    };
                    imageRepository.Update(model.Id, photo);
                    return RedirectToAction("index");
                }
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

       [HttpGet]
        public ActionResult Delete(int id)
        {
            var photo = imageRepository.Find(id);
            if(photo==null)
            {
                return NotFound();
            }
            return View(photo);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                imageRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        string UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "images/projectimages");
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
                string uploads = Path.Combine(hosting.WebRootPath, "images/projectimages");
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
