
using System.ComponentModel.DataAnnotations;
using test_backend.Models;

namespace test_backend.Data.DTOs;

public class CreateOrderDTO
{
    [Required(ErrorMessage = "É necessário enviar uma lista de produtos.")]
    [MinLength(1, ErrorMessage = "Pelo menos um produto")]

    public List<ProductQuantity> Products {get; set;}
    [Required(ErrorMessage ="PaymentMethod é obrigatório")]
    public PaymentMethod? PaymentMethod {get; set;}

}