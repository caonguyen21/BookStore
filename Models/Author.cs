using System.ComponentModel.DataAnnotations;

namespace BookStore.Models;

public partial class Author
{
    public int AuthorId { get; set; }

    [Required(ErrorMessage = "The Email field is required.")]
    [EmailAddress(ErrorMessage = "Invalid email.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "The Name field is required.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "The Status field is required.")]
    public string? Status { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
