using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDar.Areas.Admin.Models.Repositories;
using MyDar.Areas.Admin.Models;
using MyDar.Areas.Admin.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace MyDar.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IApplicationRepository<Profile> repository;
        private readonly IHostingEnvironment hosting;

        public ProfileController(IApplicationRepository<Profile> repository, IHostingEnvironment hosting)
        {
            this.repository = repository;
            this.hosting = hosting;
        }
        // GET: ProfileController
        public ActionResult Index()
        {

            var profile = repository.List();
            return View(profile);
        }

        public ActionResult Details(int id)
        {
            var profile = repository.Find(id);
            if(profile==null)
            {
                return NotFound();
            }
            return View(profile);
        }

        // GET: ProfileController/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateProfileViewModel model)
        {
            try
            {
                string FileName =UploadFile(model.File)?? string.Empty;
                if(ModelState.IsValid)
                {

                    var profile = new Profile
                    {
                        AboutUs=model.AboutUs,
                        Image=FileName,
                        Address1=model.Address1,
                        Address2=model.Address2,
                        Address3=model.Address3,
                        Email=model.Email,
                        Phone=model.Phone,
                        Mission=model.Mission,
                        Vission=model.Vission,
                        Care=model.Care,
                        Plan=model.Plan,
                        ClientNum=model.ClientNum,
                        WorkerNum=model.WorkerNum,
                        ProjectsNum=model.ProjectsNum,
                        HoursNum=model.HoursNum
                    };
                    repository.Add(profile);
                    return RedirectToAction("index");
                }
                ModelState.AddModelError("", "Please review the input fields");
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
            var profile = repository.Find(id);
            var model = new EditProfileViewModel
            {
                AboutUs = profile.AboutUs,
                Image = profile.Image,
                Address1 = profile.Address1,
                Address2 = profile.Address2,
                Address3 = profile.Address3,
                Email = profile.Email,
                Phone = profile.Phone,
                Mission = profile.Mission,
                Vission = profile.Vission,
                Care = profile.Care,
                Plan = profile.Plan,
                ClientNum = profile.ClientNum,
                WorkerNum = profile.WorkerNum,
                ProjectsNum = profile.ProjectsNum,
                HoursNum = profile.HoursNum
            };
            return View(model);
        }

        // POST: ProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditProfileViewModel model)
        {
            try
            {
                string FileName = UploadFile(model.File, model.Image);
                if(ModelState.IsValid)
                {
                    
                    var profile = new Profile
                    {
                        AboutUs=model.AboutUs,
                        Image = FileName,
                        Address1 = model.Address1,
                        Address2 = model.Address2,
                        Address3 = model.Address3,
                        Email = model.Email,
                        Phone = model.Phone,
                        Mission = model.Mission,
                        Vission = model.Vission,
                        Care = model.Care,
                        Plan = model.Plan,
                        ClientNum = model.ClientNum,
                        WorkerNum = model.WorkerNum,
                        ProjectsNum = model.ProjectsNum,
                        HoursNum = model.HoursNum
                    };
                    repository.Update(model.ID, profile);
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Please review the input Fields");
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: ProfileController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProfileController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
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
                string uploads = Path.Combine(hosting.WebRootPath, "images");
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
                string uploads = Path.Combine(hosting.WebRootPath, "images");
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
