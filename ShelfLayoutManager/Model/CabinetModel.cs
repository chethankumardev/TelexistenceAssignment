using System;
namespace ShelfLayoutManager.Model
{
    /// <summary>
    /// Represents a cabinet model.
    /// </summary>
    public class CabinetModel
	{
        public int Number { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int PositionZ { get; set; }
        public int Width { get; set; }
        public int Depth { get; set; }
        public int Height { get; set; }
    }
}

