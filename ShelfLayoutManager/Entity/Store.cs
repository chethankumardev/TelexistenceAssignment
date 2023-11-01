using System.ComponentModel.DataAnnotations;

namespace ShelfLayoutManager.Entity
{
    public class Store
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }
}
