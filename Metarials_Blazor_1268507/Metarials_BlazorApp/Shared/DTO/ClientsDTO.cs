using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Metarials_BlazorApp.Shared.DTO
{
    public class ClientsDTO
    {

        [Key]
        public int ClientID { get; set; }
        [Required, StringLength(50), Display(Name = "Client Name")]
        public string ClientName { get; set; } = default!;
        [Required, StringLength(150)]
        public string Address { get; set; } = default!;


        [Required, StringLength(50), EmailAddress]
        public string Email { get; set; } = default!;

        public bool CanDelete { get; set; }
    }
}
