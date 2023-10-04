using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRent.Shared.DTO
{
    public class RentItemEditDTO
    {
        [Key]
        public int RentID { get; set; }
        [Required]

        public int FlatID { get; set; } = default!;
        [Required]

        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
