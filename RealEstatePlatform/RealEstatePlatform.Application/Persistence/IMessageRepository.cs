using RealEstatePlatform.Application.Contracts;
using RealEstatePlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePlatform.Application.Persistence
{
    public interface IMessageRepository : IAsyncRepository<Message>
    {
    }
}
