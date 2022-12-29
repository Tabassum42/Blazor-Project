using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Metarials_BlazorApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Metarials_BlazorApp.Shared.DTO
{
    public class OrderViewDTO
    {
        [Key]
        public int OrderID { get; set; }
        [Required, Column(TypeName = "date"),
        Display(Name = "Order Date"),
            DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
            ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; } = DateTime.Today;
        [Column(TypeName = "date"),
            Display(Name = "Delivery Date"),
            DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
            ApplyFormatInEditMode = true)]
        public DateTime? DeliveryDate { get; set; }
        [Required, EnumDataType(typeof(Status))]
        public Status Status { get; set; } = Status.Pending;
        [Required]
        public string ClientName { get; set; } = default!;
        public int ItemCount { get; set; }
        public decimal OrderValue { get; set; }
    }
}
