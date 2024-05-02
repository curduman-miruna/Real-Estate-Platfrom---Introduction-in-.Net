using MediatR;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RealEstatePlatform.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        private ISender mediator = null!;
        protected virtual ISender Mediator
        {
            get
            {
                if (mediator == null)
                {
                    mediator = HttpContext?.RequestServices.GetRequiredService<ISender>();
                }
                return mediator!;
            }
        }
    }
}

