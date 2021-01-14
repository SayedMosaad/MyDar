using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyDar.ViewModels;
using MyDar.Areas.Admin.Models;
using MyDar.Areas.Admin.Models.Repositories;

namespace MyDar.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IApplicationRepository<Blogs> blogRepository;
        private readonly IApplicationRepository<Profile> profileRepository;

        public BlogsController(IApplicationRepository<Blogs> BlogRepository, IApplicationRepository<Profile> ProfileRepository)
        {
            blogRepository = BlogRepository;
            profileRepository = ProfileRepository;
        }
        public IActionResult Index()
        {
            var model = new IndexViewModel
            {
                Blogs = blogRepository.List().ToList(),
                Profiles = profileRepository.List().ToList()
            };
            return View(model);
        }
        public IActionResult BlogDetails(int id)
        {
            var blog = blogRepository.Find(id);
            var model = new IndexViewModel
            {
                Blog = blog,
                Profiles = profileRepository.List().ToList()
            };
            return View(model);
        }
    }
}
