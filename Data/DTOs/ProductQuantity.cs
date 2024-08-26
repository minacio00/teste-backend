
using System.ComponentModel.DataAnnotations;
using test_backend.Models;

namespace test_backend.Data.DTOs;

public class ProductQuantity
{
    [Required(ErrorMessage = "ProductId is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "ProductId must be a positive integer.")]
    public int ProductId {get; set;}
    [Required(ErrorMessage = "Quantity is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
    public int Quantity {get; set;}
    
}