using System.Net;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using Aplicacion.ManejadorError;

namespace Aplicacion.Documentos
{
    public class ObtenerArchivo
    {
        public class Ejecuta : IRequest<ArchivoGenerico>{
            public Guid Id {get;set;}
        }

        public class Manejador : IRequestHandler<Ejecuta, ArchivoGenerico>
        {
            private readonly CurosOnlineContext _context;
            public Manejador(CurosOnlineContext context){
                _context = context;
            }
            
            public async Task<ArchivoGenerico> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var archivo = await _context.Documento.Where(x => x.ObjetoReferencia == request.Id).FirstOrDefaultAsync();
                if(archivo == null) {
                    throw new  ManejadorExepcion(HttpStatusCode.NotFound, new {mensaje ="No se encontro la imagen"});
                }

                var archivoGenerico = new ArchivoGenerico{
                    Data =  Convert.ToBase64String(archivo.Contenido),
                    Nombre = archivo.Nombre,
                    Extension = archivo.Extenxion
                };

                return archivoGenerico;
            }
        }

    }
}