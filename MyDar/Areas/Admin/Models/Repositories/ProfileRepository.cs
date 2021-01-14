using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDar.Areas.Admin.Models.Repositories
{
    public class ProfileRepository : IApplicationRepository<Profile>
    {
        private readonly ApplicationDBContext db;

        public ProfileRepository(ApplicationDBContext db)
        {
            this.db = db;
        }
        public void Add(Profile entity)
        {
            db.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var profile = Find(id);
            db.Profiles.Remove(profile);
            db.SaveChanges();
        }

        public Profile Find(int id)
        {
            var profile = db.Profiles.FirstOrDefault(m => m.ID==id);
            return profile;
        }

        public IList<Profile> List()
        {
            return db.Profiles.ToList();
        }

        public void Update(int id, Profile entity)
        {
            var profile = Find(id);
            profile.AboutUs = entity.AboutUs;
            profile.Image = entity.Image;
            profile.Email = entity.Email;
            profile.Phone = entity.Phone;
            profile.Address1 = entity.Address1;
            profile.Address2 = entity.Address2;
            profile.Address3 = entity.Address3;
            profile.Care = entity.Care;
            profile.Vission = entity.Vission;
            profile.Mission = entity.Mission;
            profile.Plan = entity.Plan;
            profile.ProjectsNum = entity.ProjectsNum;
            profile.ClientNum = entity.ClientNum;
            profile.WorkerNum = entity.WorkerNum;
            profile.HoursNum = entity.HoursNum;
            db.SaveChanges();

        }
    }
}