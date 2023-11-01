using System;
namespace ShelfLayoutManager.Model
{
    /// <summary>
    /// Represents a row model.
    /// </summary>
	public class RowModel
	{
        public int Number { get; set; }
        public int PositionZ { get; set; }
        public int Height { get; set; }
        public int CabinetId { get; set; }
    }
}

