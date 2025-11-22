using System.ComponentModel.DataAnnotations;

namespace VmoragaCollectionManager.Models
{
    public class WishlistItem
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public int CollectionItemId { get; set; }
        public bool Owned { get; set; } // true = lo tengo, false = wishlist
        public CollectionItem CollectionItem { get; set; }
    }
}
