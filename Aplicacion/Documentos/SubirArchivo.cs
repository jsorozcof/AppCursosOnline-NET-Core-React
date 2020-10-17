using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Documentos
{
    public class SubirArchivo
    {
        public class Ejecuta : IRequest {
            public Guid? ObjetoReferencia {get;set;}
            public string Data {get;set;}
            public string Nombre {get;set;}
            public string Extension {get;set;}
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            
            private readonly CurosOnlineContext _context;
            public Manejador(CurosOnlineContext context) {
                _context = context;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var documento = await _context.Documento.Where(x => x.ObjetoReferencia == request.ObjetoReferencia).FirstOrDefaultAsync();
                if(documento == null) {
                    var doc = new Documento{
                        Contenido =  Convert.FromBase64String(request.Data),
                        Nombre = request.Nombre,
                        Extenxion = request.Extension,
                        DocumentoId = Guid.NewGuid(),
                        FechaCreacion = DateTime.UtcNow,
                        ObjetoReferencia = request.ObjetoReferencia ?? Guid.Empty
                    };
                    _context.Documento.Add(doc);
                }else{
                    documento.Contenido = Convert.FromBase64String(request.Data);
                    documento.Nombre = request.Nombre;
                    documento.Extenxion = request.Extension;
                    documento.FechaCreacion = DateTime.UtcNow;
                }

               var resultado = await _context.SaveChangesAsync();
                
                if(resultado>0){
                    return Unit.Value;
                }

                throw new Exception("No se pudo guardar el archivo");
            }
        }
    }
}