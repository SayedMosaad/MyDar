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
    public class ProjectsController : Controller
    {
        private readonly IApplicationRepository<Projects> projectrepository;
        private readonly IApplicationRepository<Categories> categoriesrepository;
        private readonly IHostingEnvironment hosting;

        public ProjectsController(IApplicationRepository<Projects> projectrepository,IApplicationRepository<Categories> categoriesrepository,IHostingEnvironment hosting)
        {
            this.projectrepository = projectrepository;
            this.categoriesrepository = categoriesrepository;
            this.hosting = hosting;
        }
        
        public ActionResult Index()
        {
            return View(projectrepository.List());
        }

        
        public ActionResult Details(int id)
        {
            var project = projectrepository.Find(id);
            return View(project);
        }

        public ActionResult Create()
        {
            var model = new CreateProjectsViewModel
            {
                Categories = FillCategories()
            };
            return View(model);
        }

        // POST: ProjectsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateProjectsViewModel model)
        {
            try
            {
                string FileName = UploadFile(model.File) ?? string.Empty;
                if(ModelState.IsValid)
                {
                    if (model.CategoryId == -1)
                    {
                        ModelState.AddModelError("", "Please Select Category");
                        return View(GetAllCategories());
                    }
                    var category = categoriesrepository.Find(model.CategoryId);
                    Projects project = new Projects
                    {
                        Name = model.Name,
                        Image = FileName,
                        Client = model.Client,
                        ProjectDate = model.ProjectDate,
                        Description = model.Description,
                        Category = category
                    };
                    projectrepository.Add(project);
                    return RedirectToAction("index");
                }
                ModelState.AddModelError("", "Please review the input fields");
                return View(GetAllCategories());
            }
            catch
            {
                return View();
            }
        }

        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var project = projectrepository.Find(id);
            if(project==null)
            {
                return NotFound();
            }
            var categoryId = project.Category == null ? project.Category.ID = 0 : project.Category.ID;
            var model = new EditProjectsViewModel
            {
                Id = project.ID,
                Name = project.Name,
                ImageUrl = project.Image,
                Client=project.Client,
                ProjectDate=project.ProjectDate,
                Description=project.Description,
                Categories=FillCategories(),
                CategoryId=categoryId
            };

            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditProjectsViewModel model)
        {
            try
            {

                string FileName = UploadFile(model.File,model.ImageUrl);
                
                if(ModelState.IsValid)
                {
                    var category = categoriesrepository.Find(model.CategoryId);
                    if (category == null)
                    {
                        ModelState.AddModelError("", "Please review the inpyt fields");
                        return View(GetAllCategories());
                    }
                    var project = new Projects
                    {
                        Name = model.Name,
                        Image=FileName,
                        Client = model.Client,
                        ProjectDate = model.ProjectDate,
                        Description = model.Description,
                        Category = category
                    };
                    projectrepository.Update(model.Id, project);
                    model.Categories = FillCategories();
                    return RedirectToAction("index");
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        
        public ActionResult Delete(int id)
        {
            var project = projectrepository.Find(id);
            if(project==null)
            {
                return NotFound();
            }
            return View(project);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                projectrepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        List<Categories> FillCategories()
        {
            var categories = categoriesrepository.List().ToList();
            categories.Insert(0, new Categories { ID = -1, Name = "--- Please Select Category ---" });
            return categories;
        }

        CreateProjectsViewModel GetAllCategories()
        {
            CreateProjectsViewModel model = new CreateProjectsViewModel
            {
                Categories = FillCategories()
            };
            return model;
        }

        string UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "images/projects");
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
                string uploads = Path.Combine(hosting.WebRootPath, "images/projects");
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
