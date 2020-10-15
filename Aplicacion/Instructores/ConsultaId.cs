using System.Net;
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia.DapperConexion.Instructor;
using Aplicacion.ManejadorError;

namespace Aplicacion.Instructores
{
    public class ConsultaId
    {
        public class Ejecuta : IRequest<InstructorModel> {
            public Guid Id  {get;set;}
        }

        public class Manejador : IRequestHandler<Ejecuta, InstructorModel>
        {
            private readonly IInstructor _instructorRepository;
            public Manejador(IInstructor instructorRepository){
                _instructorRepository = instructorRepository;
            }

            public async Task<InstructorModel> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var instructor = await _instructorRepository.ObtenerPorId(request.Id);
                if(instructor==null){
                    throw new ManejadorExepcion(HttpStatusCode.NotFound, new {mensaje="No se encontro el instructor"});
                }

                return instructor;
            }
        }

    }
}