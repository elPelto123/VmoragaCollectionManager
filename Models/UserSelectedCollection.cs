using System.ComponentModel.DataAnnotations;

namespace VmoragaCollectionManager.Models
{
    public class UserSelectedCollection
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public int CollectionId { get; set; }
        public Collection Collection { get; set; }
    }
}
