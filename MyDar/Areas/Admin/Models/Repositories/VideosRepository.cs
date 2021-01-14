using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyDar.Areas.Admin.Models;

namespace MyDar.Areas.Admin.Models.Repositories
{
    public class VideosRepository : IApplicationRepository<Videos>
    {
        private readonly ApplicationDBContext db;

        public VideosRepository(ApplicationDBContext db)
        {
            this.db = db;
        }
        public void Add(Videos entity)
        {
            db.Videos.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var video = Find(id);
            db.Videos.Remove(video);
            db.SaveChanges();
        }

        public Videos Find(int id)
        {
            var video=db.Videos.FirstOrDefault(m => m.ID == id);
            return video;
        }

        public IList<Videos> List()
        {
            return db.Videos.ToList();
        }

        public void Update(int id, Videos entity)
        {
            var video = Find(id);
            video.Video = entity.Video;
            video.Title1 = entity.Title1;
            video.Description1 = entity.Description1;
            video.Title2 = entity.Title2;
            video.Description2 = entity.Description2;
            db.SaveChanges();
        }
    }
}
