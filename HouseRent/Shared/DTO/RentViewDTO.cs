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
    public class RentViewDTO
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
        public string TenantName { get; set; } = default!;
        public int ItemCount { get; set; }
        public decimal RentValue { get; set; }

    }
}
