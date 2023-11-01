using System;
namespace ShelfLayoutManager.Model
{
    /// <summary>
    /// Represents a lane model.
    /// </summary>
	public class LaneModel
	{
        public int Number { get; set; }
        public string JanCode { get; set; }
        public int Quantity { get; set; }
        public int PositionX { get; set; }
        public int RowId { get; set; }
    }
}

