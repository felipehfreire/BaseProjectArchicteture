using System.ComponentModel.DataAnnotations;

namespace Core.Application.ViewModels.Product
{
    public class ProductViewModel
    {

        [Key]
        public int Id{ get; set; }
        [Required(ErrorMessage ="Descrição é obrigatório")]
        [Display(Name ="Descrição")]
        [MaxLength(500, ErrorMessage = "O tamanho máximo para descrição é {1}")]
        public string Description { get;  set; }

        public string InternalCode { get;set; }
        public string BarCode { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SalePrice { get; set; }

        public int? categoryId { get; set; }
        public int? productTypeId { get; set; }

        
    }
}
