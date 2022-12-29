using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public class MetarialEditDTO
    {
        [Key]
        public int MetarialID { get; set; }
        [Required, StringLength(50), Display(Name = "Metarial Name")]
        public string MetarialName { get; set; } = default!;
        [Required, Column(TypeName = "money"), DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal Price { get; set; }
        [StringLength(150)]
        public string Picture { get; set; } = default!;
        public bool IsAvailable { get; set; }
    }
}
