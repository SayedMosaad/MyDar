using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDar.Areas.Admin.Models.Repositories
{
    public class BlogsRepository : IApplicationRepository<Blogs>
    {
        private readonly ApplicationDBContext db;

        public BlogsRepository(ApplicationDBContext db)
        {
            this.db = db;
        }
        public void Add(Blogs entity)
        {
            db.Blogs.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var blog = Find(id);
            db.Blogs.Remove(blog);
            db.SaveChanges();
        }

        public Blogs Find(int id)
        {
            var blog = db.Blogs.FirstOrDefault(m => m.ID == id);
            return blog;
        }

        public IList<Blogs> List()
        {
            return db.Blogs.ToList();
        }

        public void Update(int id, Blogs entity)
        {
            var blog = Find(id);
            blog.Title = entity.Title;
            blog.Description = entity.Description;
            blog.Image = entity.Image;
            blog.Body = entity.Body;
            db.SaveChanges();
        }
    }
}
