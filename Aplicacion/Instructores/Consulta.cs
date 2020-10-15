using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia.DapperConexion.Instructor;
using System.Linq;

namespace Aplicacion.Instructores
{
    public class Consulta
    {
        public class Lista : IRequest<List<InstructorModel>> {}

        public class Manejador : IRequestHandler<Lista, List<InstructorModel>>
        {
            private readonly IInstructor _instructorRepository;
            public Manejador(IInstructor instructorRepository){
                   _instructorRepository = instructorRepository; 
            }
            public async Task<List<InstructorModel>> Handle(Lista request, CancellationToken cancellationToken)
            {
                  var resultado = await _instructorRepository.ObtenerLista();
                  return resultado.ToList();
                 
            }
        }

    }
}