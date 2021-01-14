using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDar.Areas.Admin.Models.Repositories
{
    public class CategoriesRepository : IApplicationRepository<Categories>
    {
        private readonly ApplicationDBContext db;

        public CategoriesRepository(ApplicationDBContext db)
        {
            this.db = db;
        }
        public void Add(Categories entity)
        {
            db.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var Category = Find(id);
            db.Categories.Remove(Category);
            db.SaveChanges();
        }

        public Categories Find(int id)
        {
            var category = db.Categories.Include(m=>m.Projects).FirstOrDefault(m => m.ID == id);
            return category;
        }

        public IList<Categories> List()
        {
            return db.Categories.Include(m=>m.Projects).ToList();
        }

        public void Update(int id, Categories entity)
        {
            var category = Find(id);
            category.Name = entity.Name;
            db.SaveChanges();
        }
    }
}
