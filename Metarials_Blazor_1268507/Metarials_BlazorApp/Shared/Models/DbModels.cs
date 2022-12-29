using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Metarials_BlazorApp.Shared.Models
{
    public enum Status { Pending = 1, Delivered, Cancelled }
    public class Clients
    {
        [Key]
        public int ClientID { get; set; }
        [Required, StringLength(50), Display(Name = "Client Name")]
        public string ClientName { get; set; } = default!;
        [Required, StringLength(150)]
        public string Address { get; set; } = default!;


        [Required, StringLength(50), EmailAddress]
        public string Email { get; set; } = default!;

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
    public class Order
    {
        public int OrderID { get; set; }
        [Required, Column(TypeName = "date"),
        Display(Name = "Order Date"),
            DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
            ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }
        [Column(TypeName = "date"),
            Display(Name = "Delivery Date"),
            DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
            ApplyFormatInEditMode = true)]
        public DateTime? DeliveryDate { get; set; }
        [Required, EnumDataType(typeof(Status))]
        public Status Status { get; set; }
        [Required, ForeignKey("Clients")]
        public int ClientID { get; set; }
        public Clients Clients { get; set; } = default!;
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
    public class OrderItem
    {
        [ForeignKey("Order")]
        public int OrderID { get; set; }
        [ForeignKey("Metarials")]
        public int MetarialID { get; set; }
        [Required]
        public int Quantity { get; set; }

        public virtual Order Order { get; set; } = default!;
        public virtual Metarials Metarials { get; set; } = default!;


    }
    public class Metarials
    {
        [Key]
        public int MetarialID { get; set; }
        [Required, StringLength(50), Display(Name = "Metarial Name")]
        public string MetarialName { get; set; } = default!;
        [Required, Column(TypeName = "money"), DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal Price { get; set; }
        [Required, StringLength(150)]
        public string Picture { get; set; } = default!;
        public bool IsAvailable { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
    public class ClientDbContext : DbContext
    {
        public ClientDbContext(DbContextOptions<ClientDbContext> options) : base(options) { }
        public DbSet<Clients> Clients { get; set; } = default!;
        public DbSet<Order> Orders { get; set; } = default!;
        public DbSet<OrderItem> OrderItems { get; set; } = default!;
        public DbSet<Metarials> Metarials { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>().HasKey(o => new { o.OrderID, o.MetarialID });
        }
    }
}
