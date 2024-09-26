using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NDMM20240911_GUIA_4.Auth;

namespace AMMA20240901.Endpoints
{
    public static class AccountEndpoint
    {
        public static void AddAccountEndpoints(this WebApplication app)
        {
            // Endpoint para iniciar sesión
            app.MapPost("/account/login", (string login, string password, IJwtAuthenticationService authService) =>
            {
                // Verificación simple de credenciales
                if (login == "admin" && password == "12345")
                {
                    var token = authService.Authenticate(login);
                    return Results.Ok(token);
                }
                else
                {
                    return Results.Unauthorized();
                }
            });

            // Endpoint para cerrar sesión (requiere autenticación)
            app.MapPost("/account/logout", [Authorize] () =>
            {
                // Aquí podrías implementar la lógica para invalidar el token si fuera necesario
                return Results.Ok("Logged out successfully");
            });
        }
    }
}
