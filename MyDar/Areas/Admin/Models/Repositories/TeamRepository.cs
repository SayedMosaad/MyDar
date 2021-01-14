using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDar.Areas.Admin.Models.Repositories
{
    public class TeamRepository : IApplicationRepository<Team>
    {
        private readonly ApplicationDBContext db;

        public TeamRepository(ApplicationDBContext db)
        {
            this.db = db;
        }
        public void Add(Team entity)
        {
            db.Team.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var team = Find(id);
            db.Team.Remove(team);
            db.SaveChanges();
        }

        public Team Find(int id)
        {
            var team = db.Team.FirstOrDefault(m => m.ID == id);
            return team;
        }

        public IList<Team> List()
        {
            return db.Team.ToList();
        }

        public void Update(int id, Team entity)
        {
            var team = Find(id);
            team.Name = entity.Name;
            team.Image = entity.Image;
            team.Job = entity.Job;
            team.Bio = entity.Bio;
            team.Facebook = entity.Facebook;
            team.Twitter = entity.Twitter;
            team.Instagram = entity.Instagram;
            team.Linkedin = entity.Linkedin;
            db.SaveChanges();
        }
    }
}
