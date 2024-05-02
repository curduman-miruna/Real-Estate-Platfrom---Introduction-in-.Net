using Infrastructure.Repositories;
using RealEstatePlatform.Application.Persistence;
using RealEstatePlatform.Domain.Entities;

namespace RealEstatePlatform.Infrastructure.Repositories
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(RealEstatePlatformContext context) : base(context)
        {
            
        }

    }
   
}
