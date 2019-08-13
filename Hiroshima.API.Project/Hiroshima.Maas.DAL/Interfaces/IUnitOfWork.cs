using Hiroshima.Maas.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiroshima.Maas.Services.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        void Complete();
        Task CompleteAsync();
    }
}