using System.Net;
using System;
namespace Aplicacion.ManejadorError
{
    public class ManejadorExepcion: Exception
    {
        public HttpStatusCode Codigo {get;}
        public object Errores {get;}

        public ManejadorExepcion(HttpStatusCode codigo, object errores = null)
        {
            Codigo = codigo;
            Errores = errores;
        }
    }
}