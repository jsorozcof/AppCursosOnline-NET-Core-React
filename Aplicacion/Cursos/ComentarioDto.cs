using System;

namespace Aplicacion.Cursos
{
    public class ComentarioDto
    {
        public Guid ComentarioId{get;set;}
        public string Alumno{get;set;}
        public int Puntaje{get;set;}
        public string ComentarioText {get;set;}
        public Guid CursoId {get;set;}

        public DateTime? FechaCreacion {get;set;}

    }
}