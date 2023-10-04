using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace HouseRent.Shared.Models
{
    public enum Status { Pending = 1, Booking, Cancelled }
    public class Tenant
    {
        public int TenantID { get; set; }
        [Required, StringLength(50), Display(Name = "Tenant Name")]
        public string TenantName { get; set; } = default!;
        [Required, StringLength(150)]
        public string Address { get; set; } = default!;


        [Required, StringLength(50), EmailAddress]
        public string Email { get; set; } = default!;

        public virtual ICollection<Rent> Rents { get; set; } = new List<Rent>();
    }
    public class Rent
    {
        public int RentID { get; set; }
        [Required, Column(TypeName = "date"),
        Display(Name = "Rent Date"),
            DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
            ApplyFormatInEditMode = true)]
        public DateTime RentDate { get; set; }
        [Column(TypeName = "date"),
            Display(Name = "Rent Date"),
            DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
            ApplyFormatInEditMode = true)]
        public DateTime? BookedDate { get; set; }
        [Required, EnumDataType(typeof(Status))]
        public Status Status { get; set; }
        [Required, ForeignKey("Tenant")]
        public int TenantID { get; set; }
        public Tenant Tenant { get; set; } = default!;
        public virtual ICollection<RentItem> RentItems { get; set; } = new List<RentItem>();

    }
    public class RentItem
    {
        [ForeignKey("Rent")]
        public int RentID { get; set; }
        [ForeignKey("Flat")]
        public int FlatID { get; set; }
        [Required]
        public int Quantity { get; set; }

        public virtual Rent Rent { get; set; } = default!;
        public virtual Flat Flat { get; set; } = default!;


    }
    public class Flat
    {
        public int FlatID { get; set; }
        [Required, StringLength(50), Display(Name = "Flat Name")]
        public string FlatName { get; set; } = default!;
        [Required, Column(TypeName = "money"), DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal Price { get; set; }
        [Required, StringLength(150)]
        public string Picture { get; set; } = default!;
        public bool IsAvailable { get; set; }
        public virtual ICollection<RentItem> RentItems { get; set; } = new List<RentItem>();
    }
    


}
