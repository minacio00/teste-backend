using System.ComponentModel.DataAnnotations;

namespace test_backend.Models;

public class Product
{

    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O preço é obrigatório.")]
    public decimal? Price { get; set; }

    [Required(ErrorMessage = "A quantidade em estoque é obrigatória.")]
    public uint? StockQuantity { get; set; }

    [Required(ErrorMessage = "A quantidade mínima em estoque é obrigatória.")]
    public uint? MinimumStockQuantity { get; set; }

}