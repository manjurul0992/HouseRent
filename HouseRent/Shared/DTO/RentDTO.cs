using Microsoft.EntityFrameworkCore.Metadata.Internal;
using HouseRent.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HouseRent.Shared.DTO
{
    public class RentDTO
    {
        [Key]
        public int RentID { get; set; }
        [Required, Column(TypeName = "date"),
        Display(Name = "Rent Date"),
            DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
            ApplyFormatInEditMode = true)]
        public DateTime RentDate { get; set; } = DateTime.Today;
        [Column(TypeName = "date"),
            Display(Name = "Rent Date"),
            DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
            ApplyFormatInEditMode = true)]
        public DateTime? BookedDate { get; set; }
        [Required, EnumDataType(typeof(Status))]
        public Status Status { get; set; } = Status.Pending;
        [Required]
        public int TenantID { get; set; }
        
        public virtual ICollection<RentItemDTO> RentItems { get; set; } = new List<RentItemDTO>();
    }
}
