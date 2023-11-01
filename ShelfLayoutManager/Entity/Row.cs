using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ShelfLayoutManager.Entity
{
    [Table("row")]
    public class Row
    {
        [Key, Required]
        public int Number { get; set; }
        public int PositionZ { get; set; }
        public int Height { get; set; }

        public ICollection<Lane> Lanes { get; set; }
        public int CabinetId { get; set; } // Foreign key to Cabinet
        public Cabinet Cabinet { get; set; }
    }
}

