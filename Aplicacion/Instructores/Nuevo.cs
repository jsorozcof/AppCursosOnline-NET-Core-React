using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Persistencia.DapperConexion.Instructor;

namespace Aplicacion.Instructores
{
    public class Nuevo
    {
        public class Ejecuta: IRequest
        {
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public string Grado { get; set; }
        }

        public class EjecutaValida : AbstractValidator<Ejecuta>
        {
            public EjecutaValida()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellidos).NotEmpty();
                RuleFor(x => x.Grado).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly IInstructor _InstructorRepository;
            public Manejador(IInstructor InstructorRepository)
            {
                _InstructorRepository = InstructorRepository;
            }
            
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var resultado = await  _InstructorRepository.Nuevo(request.Nombre, request.Apellidos, request.Grado);
                if(resultado > 0)
                {
                    return Unit.Value;

                }
               
                throw new Exception("No se pudo insertar el instructor");
            }
        }
    }
}