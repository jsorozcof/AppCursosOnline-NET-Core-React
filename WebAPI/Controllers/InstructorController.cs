using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Persistencia.DapperConexion.Instructor;
using Aplicacion.Instructores;
using MediatR;
using System;
using Microsoft.AspNetCore.Authorization;


namespace WebAPI.Controllers
{
    public class InstructorController: MiControllerBase
    {
      
       [Authorize(Roles = "Admin")]
       [HttpGet]   
       public async Task<ActionResult<List<InstructorModel>>> ObtenerInstructores(){
           return await Mediador.Send(new Consulta.Lista());
       }


       [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
           return await Mediador.Send(data);     
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Actualizar(Guid id, Editar.Ejecuta data){
            data.InstructorId = id;
           return await Mediador.Send(data);  
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(Guid id){
            return await Mediador.Send(new Eliminar.Ejecuta{Id = id});
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InstructorModel>> ObtenerPorId(Guid id){
            return await Mediador.Send(new ConsultaId.Ejecuta{Id = id});
        }
    }
}