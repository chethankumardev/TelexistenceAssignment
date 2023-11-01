using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ShelfLayoutManager.Entity
{
    [Table("lane")]
    public class Lane
    {
        [Key, Required]
        public int Number { get; set; }
        public string JanCode { get; set; }
        public int Quantity { get; set; }
        public int PositionX { get; set; }

        public int RowId { get; set; } // Foreign key to Row
        public Row Row { get; set; }
    }
}

