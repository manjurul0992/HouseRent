using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRent.Shared.DTO
{
    public class RentItemDTO
    {
        [ForeignKey("Rent")]
        public int RentID { get; set; }
        [ForeignKey("Flat")]
        public int FlatID { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
