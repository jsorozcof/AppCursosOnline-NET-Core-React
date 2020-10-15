using System.Net;
using System;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Persistencia;
using Aplicacion.ManejadorError;
using System.Linq;

namespace Aplicacion.Cursos
{
    public class Eliminar
    {
        public class Ejecuta: IRequest
        {
            public Guid Id { get; set; }

        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CurosOnlineContext _context;
            public Manejador(CurosOnlineContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var instructorDB = _context.CursoInstructor.Where(x => x.CursoId == request.Id);
                foreach(var instructor in instructorDB)
                {
                    _context.CursoInstructor.Remove(instructor);
                }

                var comentasriosDB = _context.Comentario.Where(x => x.CursoId == request.Id);
                foreach(var cmt in comentasriosDB)
                {
                    _context.Comentario.Remove(cmt);
                }

                var precioDB = _context.Precio.Where(x => x.CursoId == request.Id).FirstOrDefault();
                if(precioDB != null)
                {
                    _context.Precio.Remove(precioDB);
                }



                var curso = await _context.Curso.FindAsync(request.Id);
                if(curso == null)
                {
                    //throw new Exception("No se puede eliminar el curso");
                    throw new ManejadorExepcion(HttpStatusCode.NotFound, new {curso = "No se encontro el curso"});
                }
                
                _context.Remove(curso);
                var resultado = await _context.SaveChangesAsync();
                if(resultado > 0)
                    return Unit.Value;

                throw new Exception("No se pudieron guardar los cambios");
            }

        }
    }
}
