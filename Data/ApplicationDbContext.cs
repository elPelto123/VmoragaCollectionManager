using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace VmoragaCollectionManager.Data;


using VmoragaCollectionManager.Models;

public class ApplicationDbContext : IdentityDbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
	{
	}

	public DbSet<Collection> Collections { get; set; }
	public DbSet<CollectionItem> CollectionItems { get; set; }
	public DbSet<WishlistItem> WishlistItems { get; set; }
	public DbSet<UserSelectedCollection> UserSelectedCollections { get; set; }
}
