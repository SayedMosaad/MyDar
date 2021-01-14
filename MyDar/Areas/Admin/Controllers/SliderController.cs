using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDar.Areas.Admin.Models.Repositories;
using MyDar.Areas.Admin.Models;
using MyDar.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace MyDar.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SliderController : Controller
    {
        private readonly IApplicationRepository<Slider> repository;
        public SliderController(IApplicationRepository<Slider> repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            var sliders = repository.List();
            return View(sliders);
        }

        // GET: SliderController/Details/5
        public ActionResult Details(int id)
        {
            var slider = repository.Find(id);
            if(slider==null)
            {
                return NotFound();
            }
            return View(slider);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateSliderViewModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var slider = new Slider
                    {
                        Title = model.Title,
                        Description = model.Description
                    };
                    repository.Add(slider);
                    return RedirectToAction("Index", "Slider");
                }
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var slider = repository.Find(id);
            if(slider==null)
            {
                return NotFound();
            }
            var ViewModel = new EditSliderViewModel
            {
                ID = slider.Id,
                Title = slider.Title,
                Description = slider.Description
            };
            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditSliderViewModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {

                    Slider slider = new Slider
                    {
                        Title = model.Title,
                        Description = model.Description
                    };
                    repository.Update(model.ID, slider);
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
            var slider = repository.Find(id);
            return View(slider);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                repository.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(repository.Find(id));
            }
        }
    }
}
