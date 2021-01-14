using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDar.Areas.Admin.Models.Repositories
{
    public interface IimagesRepository:IApplicationRepository<Photos>
    {
        public IList<Photos> GetImages(int id);
    }

    public class ImagesRepository : IimagesRepository
    {
        private readonly ApplicationDBContext db;

        public ImagesRepository(ApplicationDBContext db)
        {
            this.db = db;
        }
        public void Add(Photos entity)
        {
            db.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var photo = Find(id);
            db.Photos.Remove(photo);
            db.SaveChanges();
        }

        public Photos Find(int id)
        {
            var photo = db.Photos.Include(a=>a.Project).FirstOrDefault(m => m.ID == id);
            return photo;
        }

        public IList<Photos> GetImages(int id)
        {
            var images = db.Photos.Where(m => m.ProjectId == id).ToList();
            return images;
        }

        public IList<Photos> List()
        {
            return db.Photos.Include(a=>a.Project).ToList();
        }

        public void Update(int id, Photos entity)
        {
            var photo = Find(id);
            photo.Image = entity.Image;
            photo.Project = entity.Project;
            db.SaveChanges();
        }
    }
}
