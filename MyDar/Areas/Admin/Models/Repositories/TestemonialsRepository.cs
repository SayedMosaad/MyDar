using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDar.Areas.Admin.Models.Repositories
{
    public class TestemonialsRepository : IApplicationRepository<Testemonials>
    {
        private readonly ApplicationDBContext db;

        public TestemonialsRepository(ApplicationDBContext db)
        {
            this.db = db;
        }
        public void Add(Testemonials entity)
        {
            db.Testemonials.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var test = Find(id);
            db.Testemonials.Remove(test);
            db.SaveChanges();
        }

        public Testemonials Find(int id)
        {
            var test = db.Testemonials.FirstOrDefault(m => m.ID == id);
            return test;
        }

        public IList<Testemonials> List()
        {
            return db.Testemonials.ToList();
        }

        public void Update(int id, Testemonials entity)
        {
            var test = Find(id);
            test.Name = entity.Name;
            test.Job = entity.Job;
            test.Image = entity.Image;
            test.Description = entity.Description;
            db.SaveChanges();
        }
    }
}
