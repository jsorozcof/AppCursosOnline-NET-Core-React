using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia.DapperConexion.Instructor;

namespace Aplicacion.Instructores
{
    public class Eliminar
    {
        public class Ejecuta : IRequest{
            public Guid Id {get;set;}
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly IInstructor _instructorRepositorio;
            public Manejador(IInstructor instructorRepositorio){
                _instructorRepositorio = instructorRepositorio;
            }
            
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                 var resultados = await _instructorRepositorio.Elimina(request.Id);
                if(resultados>0){
                    return Unit.Value;
                }

                throw new Exception("No se pudo eliminar el instructor");
            }
        }

    }
}