namespace BookStore.Models;

public partial class Author
{
    public int AuthorId { get; set; }

    public string? Email { get; set; }

    public string? Name { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
