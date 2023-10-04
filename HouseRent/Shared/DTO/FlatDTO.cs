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
    public class FlatDTO
    {
        public int FlatID { get; set; }
        [Required, StringLength(50), Display(Name = "Flat Name")]
        public string FlatName { get; set; } = default!;
        [Required, Column(TypeName = "money"), DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal Price { get; set; }
        [Required, StringLength(150)]
        public string Picture { get; set; } = default!;
        public bool IsAvailable { get; set; }
        
    }
}
