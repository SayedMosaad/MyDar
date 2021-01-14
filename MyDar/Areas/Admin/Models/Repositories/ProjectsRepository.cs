using Microsoft.EntityFrameworkCore;
//using Octopus.Client.Repositories.Async;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDar.Areas.Admin.Models.Repositories
{


    public class ProjectsRepository : IApplicationRepository<Projects>
    {
        private readonly ApplicationDBContext db;

        public ProjectsRepository(ApplicationDBContext db)
        {
            this.db = db;
        }
        public void Add(Projects entity)
        {
            db.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var project = Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
        }

        public Projects Find(int id)
        {
            var Project = db.Projects.Include(a=>a.Photos).Include(m=>m.Category).FirstOrDefault(m => m.ID == id);
            return Project;
        }


        public IList<Projects> List()
        {
            return db.Projects.Include(m => m.Photos).Include(m => m.Category).ToList();
        }

        public void Update(int id, Projects entity)
        {
            var project = Find(id);
            project.Name = entity.Name;
            project.ProjectDate = entity.ProjectDate;
            project.Image = entity.Image;
            project.Client = entity.Client;
            project.Description = entity.Description;
            project.Category = entity.Category;
            db.SaveChanges();
        }
    }

    
}
