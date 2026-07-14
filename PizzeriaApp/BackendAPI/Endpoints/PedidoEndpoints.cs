using Shared;
using Microsoft.AspNetCore.Builder;
namespace BackendAPI;

public static class PedidoEndpoints
{
    private static List<Pedido> pedidos = new List<Pedido>();
    public static void MapPedidoEndpoints(this WebApplication app)
    {
        app.MapPost("/pedido", (CrearPedidosDTO pedidoDTO) =>
        {
            if (pedidoDTO == null)
            {
                return Results.BadRequest("El pedido no puede ser nulo.");
            }

            var nuevoPedido = new Pedido
            {
                Id = pedidos.Count + 1,
                ClienteId = pedidoDTO.ClienteId,
                PizzaId = pedidoDTO.PizzaId,
                Fecha = DateTime.Now,
                EstadoPedidos = EstadoPedidos.Pendiente,
                Detalles = pedidoDTO.Detalles,
                Pagado = false,
                Total = 0 // varia dependiendo la cantidad de pizzas y el precio de cada una.
            };

            pedidos.Add(nuevoPedido);
            return Results.Created($"/pedido/{nuevoPedido.Id}", nuevoPedido);
        });

        app.MapGet("/pedido", (EstadoPedidos? estado, int? clienteId) =>
        {
            IEnumerable<Pedido> pedidosFiltrados = pedidos;
            if (estado is not null)
            {
                pedidosFiltrados = pedidosFiltrados.Where(p => p.EstadoPedidos == estado);
            }

            if (clienteId is not null)
            {
                pedidosFiltrados = pedidosFiltrados.Where(p => p.ClienteId == clienteId);
            }
            return Results.Ok(pedidosFiltrados);
        });

        app.MapGet("/pedido/{id;int}", (int id) =>
        {
            var ticket = pedidos.FirstOrDefault(p => p.Id == id);
            if (ticket is null)
            {
                return Results.NotFound($"No se encontró un pedido con el ID {id}.");
            }
            return Results.Ok(ticket);
        });

        app.MapPut("/pedido/{id:int}", (int id, EstadoPedidos nuevoEstado) =>
        {
            var pedido = pedidos.FirstOrDefault(p => p.Id == id);
            if (pedido is null)
            {
                return Results.NotFound($"No se encontró un pedido con el ID {id}.");
            }

            pedido.EstadoPedidos = nuevoEstado;
            return Results.Ok(new {mensaje = "El estado a sido actualizado correctamente.", pedido});
        });
    }

}