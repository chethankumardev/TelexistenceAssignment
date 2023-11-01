using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ShelfLayoutManager.Entity
{
	[Table("cabinet")]
	public class Cabinet
	{
		[Key , Required]
        public int Number { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int PositionZ { get; set; }
        public int Width { get; set; }
        public int Depth { get; set; }
        public int Height { get; set; }

        public ICollection<Row> Rows { get; set; }

    }
}

