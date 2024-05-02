using MediatR;
using RealEstatePlatform.Application.Features.Images.Queries.GetAll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePlatform.Application.Features.Images.Queries.GetImagesByPropertyId
{
    public class GetImagesByPropertyIdQuery() : IRequest<GetImagesByPropertyIdResponse>
    {
        public Guid PropertyId { get; set; }
    }
}
