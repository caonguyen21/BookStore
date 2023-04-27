namespace BookStore.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
