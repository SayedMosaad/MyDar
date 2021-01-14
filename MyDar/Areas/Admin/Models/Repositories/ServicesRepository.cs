using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDar.Areas.Admin.Models.Repositories
{
    public class ServicesRepository : IApplicationRepository<Services>
    {
        private readonly ApplicationDBContext db;

        public ServicesRepository( ApplicationDBContext db)
        {
            this.db = db;
        }

        public void Add(Services entity)
        {
            db.Services.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var service = Find(id);
            db.Services.Remove(service);
            db.SaveChanges();
        }

        public Services Find(int id)
        {
            var Service = db.Services.FirstOrDefault(m => m.ID == id);
            return Service;
        }

        public IList<Services> List()
        {
            return db.Services.ToList();
        }

        public void Update(int id, Services entity)
        {
            var service = Find(id);
            service.Title = entity.Title;
            service.Description = entity.Description;
            db.SaveChanges();
        }
    }
}
