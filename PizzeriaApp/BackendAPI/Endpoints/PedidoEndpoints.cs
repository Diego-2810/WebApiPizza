using Shared;
using Microsoft.AspNetCore.Builder;
namespace BackendAPI;

public static class PedidoEndpoints
{
    public static void MapPedidoEndpoints(this WebApplication app)
    {
        app.MapPost("/api/pedidos", (CrearPedidosDTO pedidoDto) =>
        {
            if (pedidoDto == null)
            {
                return Results.BadRequest();
            }
            return Results.Created($"/api/pedidos/{pedidoDto.ClienteId}", pedidoDto);
        });
    }
}