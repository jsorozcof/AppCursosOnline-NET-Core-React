using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MiControllerBase: ControllerBase
    {
        private  IMediator _mediador;
        protected IMediator Mediador => _mediador ?? (_mediador = HttpContext.RequestServices.GetService<IMediator>());
        
    }
}