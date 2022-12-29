using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metarials_BlazorApp.Shared.DTO
{
    
       public class OrderItemEditDTO
    {
        [Key]
        public int OrderID { get; set; }

        [Required]
        public int MetarialID { get; set; } 
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
