using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDar.Areas.Admin.Models.Repositories
{
    public interface IApplicationRepository<TEntity>
    {
        IList<TEntity> List();
        TEntity Find(int id);
        void Add(TEntity entity);
        void Delete(int id);
        void Update(int id, TEntity entity);

    }
}
