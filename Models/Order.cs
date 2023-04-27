namespace BookStore.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? CustomerId { get; set; }

    public DateTime? OrderDate { get; set; }

    public decimal? TotalPrice { get; set; }

    public int? Quantity { get; set; }

    public string? Status { get; set; }

    public string? Image { get; set; }

    public virtual Customer? Customer { get; set; }
}
