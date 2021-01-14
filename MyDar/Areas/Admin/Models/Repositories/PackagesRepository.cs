using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDar.Areas.Admin.Models.Repositories
{
    public class PackagesRepository : IApplicationRepository<Packages>
    {
        private readonly ApplicationDBContext db;

        public PackagesRepository(ApplicationDBContext db)
        {
            this.db = db;
        }
        public void Add(Packages entity)
        {
            db.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var package = Find(id);
            db.Packages.Remove(package);
            db.SaveChanges();
        }

        public Packages Find(int id)
        {
            var package = db.Packages.FirstOrDefault(m => m.ID == id);
            return package;
        }

        public IList<Packages> List()
        {
            return db.Packages.ToList();
        }

        public void Update(int id, Packages entity)
        {
            var package = Find(id);
            package.Title = entity.Title;
            package.Description = entity.Description;
            package.Price = entity.Price;
            db.SaveChanges();
            
        }
    }
}
