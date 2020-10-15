using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad
{
    public class RolEliminar
    {
        public class Ejecuta : IRequest{
            public string Nombre {get;set;}
        }

        public class EjecutaValida : AbstractValidator<Ejecuta>{
            public EjecutaValida(){
                RuleFor(x => x.Nombre).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly RoleManager<IdentityRole> _roleManager;
            public Manejador(RoleManager<IdentityRole> roleManager){
                _roleManager = roleManager;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var role = await _roleManager.FindByNameAsync(request.Nombre);
                if(role == null){
                    throw new ManejadorExepcion(HttpStatusCode.BadRequest,new {mensaje="No existe el rol"});
                }

                var resultado = await _roleManager.DeleteAsync(role);
                if(resultado.Succeeded){
                    return Unit.Value;
                }

                throw new System.Exception("No se pudo eliminar el rol");
            }
        }
    }
}