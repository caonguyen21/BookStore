using System.ComponentModel.DataAnnotations;

namespace BookStore.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }

    [Required(ErrorMessage = "The Name field is required.")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "The Address field is required.")]
    public string? Address { get; set; }

    [Required(ErrorMessage = "The Phone field is required.")]
    [Range(1000000000, 999999999999, ErrorMessage = "The Phone field must be between 10 and 12 digits.")]
    public long? Phone { get; set; }

    [Required(ErrorMessage = "The Email field is required.")]
    [EmailAddress(ErrorMessage = "Invalid email.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "The Status field is required.")]
    public string? Status { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
