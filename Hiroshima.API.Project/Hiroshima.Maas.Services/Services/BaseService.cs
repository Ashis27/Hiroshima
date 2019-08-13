using AutoMapper;
using Hiroshima.Maas.Common.Infrastructure.MessageHandler;
using Hiroshima.Maas.Common.Infrastructure.Logger;
using Hiroshima.Maas.DAL.Repositories.Interfaces;
using Hiroshima.Maas.DL.Entities;
using Hiroshima.Maas.Services.Interfaces;
using Hiroshima.Maas.Services.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hiroshima.Maas.Services.Services
{
    public class BaseService
    {
        protected readonly IMessageHandler _messageHandler;
        protected readonly IMapper _mapper;
        protected ILoggerManager _logger;
        protected readonly IJwtFactory _jwtFactory;
        public BaseService(IMessageHandler messageHandler, IMapper mapper, ILoggerManager logger, IJwtFactory jwtFactory)
        {
            _messageHandler = messageHandler;
            _mapper = mapper;
            _logger = logger;
            _jwtFactory = jwtFactory;
        }
    }
}
