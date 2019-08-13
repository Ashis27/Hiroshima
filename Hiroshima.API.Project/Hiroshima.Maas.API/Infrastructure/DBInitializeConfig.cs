using Hiroshima.Maas.DL.Interfaces.IServices;
using Hiroshima.Maas.DL.ViewModels.LoginViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hiroshima.Maas.API.Infrastructure
{
    public class DBInitializeConfig
    {
        private IUserMap userMap;

        public DBInitializeConfig(IUserMap _userMap)

        {

            userMap = _userMap;

        }

        public void DataTest()

        {

            Users();

        }

        private void Users()

        {

            userMap.Create(new UserViewModel() { id = 1, name = "Pablo" });

            userMap.Create(new UserViewModel() { id = 2, name = "Diego" });

        }

    }
}

