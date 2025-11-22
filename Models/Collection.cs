using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VmoragaCollectionManager.Models
{
    public class Collection
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; } // Relación con usuario
        public List<CollectionItem> Items { get; set; } = new List<CollectionItem>();

        // IDs de usuarios con los que se comparte la colección
        public List<string> SharedWithUserIds { get; set; } = new List<string>();
    }
}
