using System.Net;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using Aplicacion.ManejadorError;
using System;
using FluentValidation;

namespace Aplicacion.Seguridad
{
    public class Registrar
    {
        public class Ejecuta: IRequest<UsuarioData> 
        {
            public string NombreCompleto { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string UserName {get; set;}
        }

        public class EjecutaValidador: AbstractValidator<Ejecuta>
        {
            public EjecutaValidador()
            {
                RuleFor(x => x.NombreCompleto).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.UserName).NotEmpty();

            }
        }



        public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
        {
            private readonly CurosOnlineContext _context;
            private readonly UserManager<Usuario> _userManager;
            private readonly IJwtGenerador _jwtGenerador;


            public Manejador(CurosOnlineContext context, UserManager<Usuario> userManager, IJwtGenerador jwtGenerador)
            {
                _context = context;
                _userManager = userManager;
                _jwtGenerador = jwtGenerador;

            }


            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var existe = await _context.Users.Where(x => x.Email == request.Email).AnyAsync();
                if(existe)
                {
                    throw new ManejadorExepcion(HttpStatusCode.BadRequest, new { mensaje = "Existe ya un usuario registrado con este email" });
                }

                var existeUserName = await _context.Users.Where(x => x.UserName == request.UserName).AnyAsync();
                if(existeUserName)
                {
                    throw new ManejadorExepcion(HttpStatusCode.BadRequest, new { mensaje = "Existe ya un usuario registrado con este usuario" });
                }

                var usuario = new Usuario
                {
                    NombreCompleto = request.NombreCompleto,
                    Email = request.Email,
                    UserName = request.UserName
                };

               var resultado = await _userManager.CreateAsync(usuario, request.Password);

               if(resultado.Succeeded)
               return new UsuarioData 
               {
                   NombreCompleto = usuario.NombreCompleto,
                   Token = _jwtGenerador.CrearToken(usuario, null),
                   Username = usuario.UserName,
                   Email = usuario.Email
               };

               throw new Exception("No se pudo agregar al nuevo usuario");
            }
        }
    }
}