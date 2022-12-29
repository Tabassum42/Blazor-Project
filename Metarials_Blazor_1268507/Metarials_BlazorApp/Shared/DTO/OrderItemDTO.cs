using Metarials_BlazorApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metarials_BlazorApp.Shared.DTO
{
    public class OrderItemDTO
    {
        [ForeignKey("Order")]
        public int OrderID { get; set; }
        [ForeignKey("Metarials")]
        public int MetarialID { get; set; }
        [Required]
        public int Quantity { get; set; }


    }
}
