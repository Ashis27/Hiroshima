using Hiroshima.Maas.DAL.Repositories;
using Hiroshima.Maas.DL.Entities.PassInformationModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiroshima.Maas.DAL.Interfaces
{
    public interface IPassActivePTOMapper
    {
        void BulkAddPTO(IEnumerable<PassActivePTO> activePTOs);
        void BulkDeletePTO(int id);
        void BulkDeletePass(int id);
    }
}
