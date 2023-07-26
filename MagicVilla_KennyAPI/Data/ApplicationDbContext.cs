using MagicVilla_KennyAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_KennyAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)
        {
            
        }
        public DbSet<Villa> Villas { get; set; }


        //Seeding our database with data.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Name = "Royal Villa",
                    Details = "Fine environment with good background",
                    Rate = 100,
                    Sqft = 300,
                    Occupancy = 12,
                    imageUrl = "",
                    Amenity = "",
                    CreateDate = DateTime.Now,
                },
                new Villa()
                {
                    Id = 2,
                    Name = "Diamon Villa",
                    Details = "Beautiful environment with swimming pool",
                    Rate = 200,
                    Sqft = 400,
                    Occupancy = 15,
                    imageUrl = "",
                    Amenity = "",
                    CreateDate = DateTime.Now,

                },
                new Villa()
                {
                    Id = 3,
                    Name = "De Villa",
                    Details = "Beautiful garden",
                    Rate = 300,
                    Sqft = 100,
                    Occupancy = 5,
                    imageUrl = "",
                    Amenity = "",
                    CreateDate = DateTime.Now,
                }
                );
                
        }
    }
}
