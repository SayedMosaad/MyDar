using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDar.Areas.Admin.Models.Repositories
{
    public class SliderRepository : IApplicationRepository<Slider>
    {
        private readonly ApplicationDBContext db;

        public SliderRepository(ApplicationDBContext db)
        {
            this.db = db;
        }
        public void Add(Slider entity)
        {
            db.Sliders.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var slider = Find(id);
            db.Sliders.Remove(slider);
            db.SaveChanges();
        }

        public Slider Find(int id)
        {
            var slider = db.Sliders.FirstOrDefault(a => a.Id == id);
            return slider;
        }

        public IList<Slider> List()
        {
            return db.Sliders.ToList();
        }

        public void Update(int id, Slider entity)
        {
            var slider = Find(id);
            slider.Title = entity.Title;
            slider.Description = entity.Description;
            db.SaveChanges();
        }
    }
}
