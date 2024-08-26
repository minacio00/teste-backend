using System.ComponentModel.DataAnnotations;

namespace test_backend.Models; 
public enum PaymentMethod
{
    Dinheiro,
    Crédito,
    Débito,
    Pix
}

public class Order
{
    [Key]
    public int Id {get; set;}
    [Required]
    public decimal? TotalAmount {get; set;}

    [Required]
    public string Status {get; set;}
    [Required]
    public DateTime OrderDate {get; set;} = DateTime.Now;
    [Required]
    public PaymentMethod PaymentMethod {get; set;}

    public ICollection<ProductOrder> ProductOrders {get;} = [];

    //com isso consigo fazer soft delete, assim quando apagar um elemento, o mesmo não será apagado do banco de dados;
    public DateTime? DeletedAt {get; set;}
}

