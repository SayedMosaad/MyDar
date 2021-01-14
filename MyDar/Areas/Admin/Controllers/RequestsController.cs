using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyDar.Areas.Admin.Models;
using MyDar.Areas.Admin.Models.Repositories;
using MyDar.Areas.Admin.ViewModels;
using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Authorization;

namespace MyDar.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class RequestsController : Controller
    {
        private readonly IApplicationRepository<Request> repository;

        public RequestsController(IApplicationRepository<Request> repository)
        {
            this.repository = repository;
        }
        public ActionResult Index(int PageNumber=1)
        {
            int PageSize = 6;
            int excludeRecord = (PageSize * PageNumber) - PageSize;
            var requests = repository.List().Skip(excludeRecord).Take(PageSize);

            var r = repository.List();
            var result = new PagedResult<Request>
            {
                Data = requests.ToList(),
                TotalItems=repository.List().Count(),
                PageNumber=PageNumber,
                PageSize=PageSize
            };
            return View(result);
        }
        
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var request = repository.Find(id);
            if(request==null)
            {
                return NotFound();
            }
            return View(request);
        }

        // POST: RequestsController/Delete/5
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
