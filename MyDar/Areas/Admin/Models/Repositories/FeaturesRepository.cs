using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDar.Areas.Admin.Models.Repositories
{
    public class FeaturesRepository : IApplicationRepository<Features>
    {
        private readonly ApplicationDBContext db;

        public FeaturesRepository(ApplicationDBContext db)
        {
            this.db = db;
        }
        public void Add(Features entity)
        {
            db.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var feature = Find(id);
            db.Features.Remove(feature);
            db.SaveChanges();
        }

        public Features Find(int id)
        {
            var feature = db.Features.FirstOrDefault(m => m.ID == id);
            return feature;
        }

        public IList<Features> List()
        {
            return db.Features.ToList();
        }

        public void Update(int id, Features entity)
        {
            var feature = Find(id);
            feature.Title = entity.Title;
            feature.Description = entity.Description;
            feature.Image = entity.Image;
            db.SaveChanges();
            
        }
    }
}
