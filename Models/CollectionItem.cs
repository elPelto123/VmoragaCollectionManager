using System.ComponentModel.DataAnnotations;

namespace VmoragaCollectionManager.Models
{
    public class CollectionItem
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; } // Ruta de la imagen referencial
        public int CollectionId { get; set; }
        public Collection Collection { get; set; }
    }
}
