using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace NDMM20240911_GUIA_4.Endpoints
{
    public class Bodega
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Ubicacion { get; set; }
    }

    public static class BodegaEndpoint
    {
        // Lista estática en memoria para almacenar las bodegas
        private static readonly List<Bodega> bodegas = new List<Bodega>();

        public static void AddBodegaEndpoints(this WebApplication app)
        {
            // Endpoint privado para crear una nueva bodega
            app.MapPost("/bodega/crear", [Authorize] async (Bodega nuevaBodega) =>
            {
                bodegas.Add(nuevaBodega);
                return Results.Ok(nuevaBodega);
            }).RequireAuthorization();

            // Endpoint privado para modificar una bodega existente
            app.MapPut("/bodega/modificar", [Authorize] async (Bodega bodegaModificada) =>
            {
                var bodega = bodegas.FirstOrDefault(b => b.Id == bodegaModificada.Id);
                if (bodega == null)
                {
                    return Results.NotFound("Bodega no encontrada.");
                }

                bodega.Nombre = bodegaModificada.Nombre;
                bodega.Ubicacion = bodegaModificada.Ubicacion;
                return Results.Ok("Bodega modificada exitosamente.");
            }).RequireAuthorization();

            // Endpoint privado para obtener una bodega por su ID
            app.MapGet("/bodega/{id}", [Authorize] async (int id) =>
            {
                var bodega = bodegas.FirstOrDefault(b => b.Id == id);
                if (bodega == null)
                {
                    return Results.NotFound("Bodega no encontrada.");
                }
                return Results.Ok(bodega);
            }).RequireAuthorization();
        }
    }
}

