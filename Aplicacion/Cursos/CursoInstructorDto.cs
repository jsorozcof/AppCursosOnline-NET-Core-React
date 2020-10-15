using System;
using Dominio;

namespace Aplicacion.Cursos
{
    public class CursoInstructorDto
    {
        public Guid CursoId { get; set; }
        public Guid InstructorId { get; set; }
    }
}