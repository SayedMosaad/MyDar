using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDar.Areas.Admin.Models;
using MyDar.Areas.Admin.Models.Repositories;
using MyDar.Areas.Admin.ViewModels;

namespace MyDar.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PackagesController : Controller
    {
        private readonly IApplicationRepository<Packages> repository;

        public PackagesController(IApplicationRepository<Packages> repository)
        {
            this.repository = repository;
        }
        public ActionResult Index()
        {
            return View(repository.List());
        }

        
        public ActionResult Details(int id)
        {
            var package = repository.Find(id);
            if(package==null)
            {
                return NotFound();
            }
            return View(package);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PackagesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatePackagesViewModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var package = new Packages
                    {
                        Title = model.Title,
                        Price = model.Price,
                        Description = model.Description
                    };
                    repository.Add(package);
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
            var package = repository.Find(id);
            var model = new EditPackagesViewModel
            {
                Title=package.Title,
                Price=package.Price,
                Id=package.ID,
                Description=package.Description
            };
            if(package==null)
            {
                return NotFound();
            }
            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditPackagesViewModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var package = new Packages
                    {
                        Title = model.Title,
                        Price = model.Price,
                        Description = model.Description
                    };
                    repository.Update(model.Id, package);
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
            var package = repository.Find(id);
            if(package==null)
            {
                return NotFound();
            }
            return View(package);
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
    }
}
