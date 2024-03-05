using System.ComponentModel.DataAnnotations;

namespace FusionStackBackEnd.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public int Price { get; set; }
        public string suplierName { get; set; }


    }
}
