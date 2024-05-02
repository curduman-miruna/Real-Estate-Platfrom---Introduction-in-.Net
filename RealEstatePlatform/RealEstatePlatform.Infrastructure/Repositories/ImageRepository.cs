using Infrastructure.Repositories;
using RealEstatePlatform.Application.Persistence;
using RealEstatePlatform.Domain.Entities;

namespace RealEstatePlatform.Infrastructure.Repositories
{
    public class ImageRepository : BaseRepository<Image>, IImageRepository
    {
        public ImageRepository(RealEstatePlatformContext dbContext) : base(dbContext)
        {
        }
    }
}
