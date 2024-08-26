using test_backend.Models;

namespace test_backend.Data.DTOs;

public class ReadOrderDTO
{
    public int Id { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; }
    public DateTime OrderDate { get; set; }
    public List<ProductDetailDTO> ProductDetails { get; set; }
    public PaymentMethod PaymentMethod {get; set;}
}

