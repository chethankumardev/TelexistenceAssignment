using System;
using System.ComponentModel.DataAnnotations;

namespace ShelfLayoutManager.Entity
{
    public class SKU
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
