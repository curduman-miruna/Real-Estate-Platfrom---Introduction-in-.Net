using MediatR;
using RealEstatePlatform.Application.Features.Properties.Commands.UpdateProperty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePlatform.Application.Features.Properties.Commands.UpdateProperty
{
    public class UpdatePropertyCommand : IRequest<UpdatePropertyCommandResponse>
    {
        public Guid PropertyId { get; set; }
        public string PropertyName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Location { get; set; } = default!;
        public int Price { get; set; }
        public Guid PropertyTypeId { get; set; }
        public Guid ListingTypeId { get; set; }
        public decimal Area { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public string Username { get; set; }
    }
}
