
using Hiroshima.Maas.DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hiroshima.Maas.DAL.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        IEnumerable<T> Where(Expression<Func<T,bool>>  exp );
        Task<IEnumerable<T>> SearchBy(Expression<Func<T, bool>> searchBy);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
   
    }
}
