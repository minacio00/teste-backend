using System.ComponentModel.DataAnnotations;

namespace test_backend.Models;

public class ProductOrder
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ProductId { get; set; }
    public Product Product { get; set; }

    [Required]
    public int OrderId { get; set; }
    public Order Order { get; set; }

    [Required]
    public int Quantity { get; set; }
}