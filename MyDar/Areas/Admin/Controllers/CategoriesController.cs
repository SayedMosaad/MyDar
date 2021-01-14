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
    public class CategoriesController : Controller
    {
        private readonly IApplicationRepository<Categories> repository;

        public CategoriesController(IApplicationRepository<Categories> repository)
        {
            this.repository = repository;
        }
        public ActionResult Index()
        {
            return View(repository.List());
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCategoriesViewModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var category = new Categories
                    {
                        Name = model.Name
                    };
                    repository.Add(category);
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
            var category = repository.Find(id);
            if(category==null)
            {
                return NotFound();
            }
            var model = new EditCategoriesViewModel
            {
                Id = category.ID,
                Name = category.Name
            };
            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditCategoriesViewModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var category = new Categories
                    {
                        Name = model.Name
                    };
                    repository.Update(model.Id, category);
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
            var category = repository.Find(id);
            if(category==null)
            {
                return NotFound();
            }
            return View(category);
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
    }
}
