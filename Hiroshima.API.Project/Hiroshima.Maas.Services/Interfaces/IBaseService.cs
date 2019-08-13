using Hiroshima.Maas.DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace Hiroshima.Maas.Services.Interfaces.IServices
{
    public interface IBaseService<T> where T : BaseEntity
    {

        Task<IEnumerable<T>> GetAsync();

        //Task<T> GetById(int id);

        //IEnumerable<T> Where(Expression<Func<T, bool>> exp);

        //void AddOrUpdate(T entry);

        //void Remove(int id);
    }
}
