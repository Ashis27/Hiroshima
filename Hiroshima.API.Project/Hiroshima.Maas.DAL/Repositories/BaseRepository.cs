using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Hiroshima.Maas.Common.Infrastructure.MessageHandler;
using Hiroshima.Maas.DAL.Contexts;
using Hiroshima.Maas.DAL.Repositories.Interfaces;
using Hiroshima.Maas.DL.Entities;
using Hiroshima.Maas.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hiroshima.Maas.DAL.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly HiroshimaMaaSDBContext _context;
        private readonly DbSet<T> _entities;
        protected readonly IMessageHandler _messageHandler;
        protected readonly IUnitOfWork _unitOfWork;

        public BaseRepository(HiroshimaMaaSDBContext context, IMessageHandler messageHandler, IUnitOfWork unitOfWork)
        {
            _context = context;
            _entities = context.Set<T>();
            _messageHandler = messageHandler;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _entities.ToListAsync();
        }
        //public IQueryable<T> GetSearchListAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        //{
        //    var query = _entities.AsNoTracking();
        //    if (includes != null)
        //        if (includes.Length > 0)
        //            foreach (var include in includes)
        //                query = query.Include(include);

        //    var count = query.Count(predicate);
        //    return query.Where(predicate);
        //}
        public async Task<T> GetById(int id)
        {
            return await _entities.AsNoTracking().SingleOrDefaultAsync(s => s.Id == id);
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> exp)
        {
            return _entities.AsNoTracking().Where(exp);
        }
        public virtual async Task<IEnumerable<T>> SearchBy(Expression<Func<T, bool>> searchBy)
        {
            var result = _entities.AsNoTracking().Where(searchBy);
            return await result?.ToListAsync();
        }
        public virtual IQueryable<T> SearchBy1(Expression<Func<T, bool>> searchBy, params Expression<Func<T, object>>[] includes)
        {
            var result = _entities.Where(searchBy);

            foreach (var includeExpression in includes)
                result = result.Include(includeExpression);
            return result;
        }
        public async void Add(T entity)
        {
            if (entity == null) throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.EntityNull)));
            await _entities.AddAsync(entity);
        }
        public async void AddRange(IEnumerable<T> entities)
        {
            if (entities == null) throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.EntityNull)));
            await _entities.AddRangeAsync(entities);
        }
        public async void Update(T entity)
        {
            if (entity == null) throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.EntityNull)));

            var oldEntity = await _context.FindAsync<T>(entity.Id);
            _context.Entry(oldEntity).CurrentValues.SetValues(entity);
        }
        //public void UpdateAsyncWithRelation(T entity, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        //{
        //    if (entity == null) throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.EntityNull), "", _messageHandler.GetMessage(ErrorMessagesEnum.NotValidInformations)));
        //    var result = _entities.Where(predicate);
        //    foreach (var includeExpression in includes)
        //        result = result.Include(includeExpression);
        //    // var oldEntity = _context.FindAsync<T>(entity.Id);
        //    _context.Entry(result.FirstOrDefault()).CurrentValues.SetValues(entity);

        //}

        public void Delete(T entity)
        {
            if (entity == null) throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.EntityNull)));
            _entities.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            if (entities == null) throw new Exception(string.Format(_messageHandler.GetMessage(ErrorMessagesEnum.EntityNull)));
            _entities.RemoveRange(entities);
        }
     
       protected virtual DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>();
        }

}
}
