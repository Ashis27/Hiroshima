using Hiroshima.Maas.Common.Infrastructure.Logger;
using Hiroshima.Maas.DAL.Contexts;
using Hiroshima.Maas.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hiroshima.Maas.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HiroshimaMaaSDBContext _context;
        private bool _disposed = false;
        private readonly ILoggerManager _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UnitOfWork(HiroshimaMaaSDBContext context, IHttpContextAccessor httpContextAccessor, ILoggerManager logger)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async Task CompleteAsync()
        {
            try
            {
                // get entries that are being Added or Updated
                var modifiedEntries = _context.ChangeTracker.Entries()
                        .Where(x => (x.State == EntityState.Added || x.State == EntityState.Modified));

                var userId = "0";
                if (_httpContextAccessor.HttpContext != null)
                    userId = _httpContextAccessor?.HttpContext?.User?.FindFirst("id")?.Value;

                foreach (var entry in modifiedEntries)
                {
                    var entity = entry.Entity;
                    PropertyInfo createdBy = entry.Entity.GetType().GetProperty("CreatedBy");
                    PropertyInfo createdDate = entry.Entity.GetType().GetProperty("CreatedDate");

                    if (entry.State == EntityState.Added)
                    {
                        if (null != createdBy && createdBy.CanWrite)
                            createdBy.SetValue(entry.Entity, userId, null);
                        if (null != createdDate && createdDate.CanWrite)
                            createdDate.SetValue(entry.Entity, DateTime.Now, null);
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        var dbValueOfEntity = this._context.Entry(entry.Entity).GetDatabaseValues();
                        var cd = dbValueOfEntity?.GetValue<DateTime?>("CreatedDate");
                        var cb = dbValueOfEntity?.GetValue<string>("CreatedBy");
                        if (null != cb)
                            createdBy.SetValue(entry.Entity, cb, null);
                        if (null != cd)
                            createdDate.SetValue(entry.Entity, cd, null);
                    }
                    PropertyInfo modifiedBy = entry.Entity.GetType().GetProperty("ModifiedBy");
                    PropertyInfo modifiedDate = entry.Entity.GetType().GetProperty("ModifiedDate");
                    if (null != modifiedBy && modifiedBy.CanWrite)
                        modifiedBy.SetValue(entry.Entity, userId, null);
                    if (null != modifiedDate && modifiedDate.CanWrite)
                        modifiedDate.SetValue(entry.Entity, DateTime.Now, null);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
        public void Complete()
        {
            try
            {
                // get entries that are being Added or Updated
                var modifiedEntries = _context.ChangeTracker.Entries()
                        .Where(x => (x.State == EntityState.Added || x.State == EntityState.Modified));

                var userId = "0";
                if (_httpContextAccessor.HttpContext != null)
                    userId = _httpContextAccessor?.HttpContext?.User?.FindFirst("id")?.Value;

                foreach (var entry in modifiedEntries)
                {
                    var entity = entry.Entity;
                    PropertyInfo createdBy = entry.Entity.GetType().GetProperty("CreatedBy");
                    PropertyInfo createdDate = entry.Entity.GetType().GetProperty("CreatedDate");

                    if (entry.State == EntityState.Added)
                    {
                        if (null != createdBy && createdBy.CanWrite)
                            createdBy.SetValue(entry.Entity, userId, null);
                        if (null != createdDate && createdDate.CanWrite)
                            createdDate.SetValue(entry.Entity, DateTime.Now, null);
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        var dbValueOfEntity = this._context.Entry(entry.Entity).GetDatabaseValues();
                        var cd = dbValueOfEntity?.GetValue<DateTime?>("CreatedDate");
                        var cb = dbValueOfEntity?.GetValue<string>("CreatedBy");
                        if (null != cb)
                            createdBy.SetValue(entry.Entity, cb, null);
                        if (null != cd)
                            createdDate.SetValue(entry.Entity, cd, null);
                    }
                    PropertyInfo modifiedBy = entry.Entity.GetType().GetProperty("ModifiedBy");
                    PropertyInfo modifiedDate = entry.Entity.GetType().GetProperty("ModifiedDate");
                    if (null != modifiedBy && modifiedBy.CanWrite)
                        modifiedBy.SetValue(entry.Entity, userId, null);
                    if (null != modifiedDate && modifiedDate.CanWrite)
                        modifiedDate.SetValue(entry.Entity, DateTime.Now, null);
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            } 
        }
        protected void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                this._disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
