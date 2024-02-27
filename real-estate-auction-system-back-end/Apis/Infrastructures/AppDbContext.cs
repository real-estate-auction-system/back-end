using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructures
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<RealEstate> RealEstates { get; set; }
        public DbSet<RealEstateImage> RealEstateImages { get; set; }
        public DbSet<RealtimeAuction> RealtimeAuctions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<TypeOfRealEstate> TypeOfRealEstates { get; set; }
    }
}
