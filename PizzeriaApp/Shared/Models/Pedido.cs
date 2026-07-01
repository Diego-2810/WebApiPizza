namespace Shared;

public class Pedido
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; }
    public int PizzaId { get; set; }
    public Pizza Pizza { get; set; }
    public DateTime Fecha { get; set; }
    public EstadoPedido EstadoPedido { get; set; } 
    public string Detalles { get; set; }
    public bool Pagado { get; set; }
    public int Total { get; set; }
}
