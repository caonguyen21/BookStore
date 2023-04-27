namespace BookStore.Models;
using System.ComponentModel.DataAnnotations;

public partial class Book
{
    public int BookId { get; set; }

    [Required(ErrorMessage = "The Title field is required.")]
    public string? Title { get; set; }

    [Required(ErrorMessage = "The AuthorId field is required.")]
    public int? AuthorId { get; set; }

    [Required(ErrorMessage = "The PublisherId field is required.")]
    public int? PublisherId { get; set; }

    [Required(ErrorMessage = "The SupplierId field is required.")]
    public int? SupplierId { get; set; }

    [Required(ErrorMessage = "The Price field is required.")]
    public decimal? Price { get; set; }

    [Required(ErrorMessage = "The Quantity field is required.")]
    public int? Quantity { get; set; }

    public string? Image { get; set; }

    [Required(ErrorMessage = "The Status field is required.")]
    public string? Status { get; set; }

    public virtual Author? Author { get; set; }

    public virtual Publisher? Publisher { get; set; }

    public virtual Supplier? Supplier { get; set; }
}
