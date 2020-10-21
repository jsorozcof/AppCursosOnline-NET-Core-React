using System;
using System.Threading.Tasks;
using Aplicacion.Documentos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;

namespace WebAPI
{
    public class DocumentoController: MiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> GuadarArchivo(SubirArchivo.Ejecuta parametros)
        {
            return await Mediador.Send(parametros);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArchivoGenerico>> Obtenerdocumento(Guid id)
        {
            return await Mediador.Send(new ObtenerArchivo.Ejecuta { Id = id });
        }
    }
}