using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDar.Areas.Admin.Models;
using MyDar.Areas.Admin.ViewModels;
using MyDar.Areas.Admin.Models.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace MyDar.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ServicesController : Controller
    {
        private readonly IApplicationRepository<Services> repository;

        public ServicesController(IApplicationRepository<Services> repository)
        {
            this.repository = repository;
        }
        
        public ActionResult Index()
        {
            return View(repository.List());
        }

       
        public ActionResult Details(int id)
        {
            var service = repository.Find(id);
            if(service==null)
            {
                return NotFound();
            }
            return View(service);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServicesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateServicesViewModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var Service = new Services
                    {
                        Title = model.Title,
                        Description = model.Description
                    };
                    repository.Add(Service);
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
            var service = repository.Find(id);
            if(service==null)
            {
                return NotFound();
            }
            EditServicesViewModel model = new EditServicesViewModel
            {
                Id=service.ID,
                Title=service.Title,
                Description=service.Description
            };
            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditServicesViewModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var service = new Services
                    {
                        Title = model.Title,
                        Description = model.Description
                    };
                    repository.Update(model.Id, service);
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: ServicesController/Delete/5
        public ActionResult Delete(int id)
        {
            var service = repository.Find(id);
            return View(service);
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
                return View(repository.Find(id));
            }
        }
    }
}
