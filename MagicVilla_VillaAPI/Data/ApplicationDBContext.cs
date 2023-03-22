using MagicVilla_VillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Data
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): base(options)
        {
                
        }
        public DbSet<Villa> Villas { get; set; }

        // fill out data rows in Villas Table

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa() {
                    Id=1,
                    Name="Royal Villa",
                    Details= "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ImageUrl= "C:\\Users\\nerme\\full-stack-learning-projects\\DotNetMastery_RESTAPIS\\ASP.NET MVC\\MagicVilla\\MagicVilla_VillaAPI\\Images\\01.jpg",
                    Rate=220,
                    Occupancy=5,
                    Sqft=550,
                    Amenity="",
                    CreatedDate= DateTime.Now
                 
                },
                 new Villa()
                 {
                     Id = 2,
                     Name = "Ghoyal Villa",
                     Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                     ImageUrl = "C:\\Users\\nerme\\full-stack-learning-projects\\DotNetMastery_RESTAPIS\\ASP.NET MVC\\MagicVilla\\MagicVilla_VillaAPI\\Images\\01.jpg",
                     Rate = 120,
                     Occupancy = 4,
                     Sqft = 230,
                     Amenity = "",
                     CreatedDate = DateTime.Now

                 },
                  new Villa()
                  {
                      Id = 3,
                      Name = "Shriya Ghoshal Villa",
                      Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                      ImageUrl = "C:\\Users\\nerme\\full-stack-learning-projects\\DotNetMastery_RESTAPIS\\ASP.NET MVC\\MagicVilla\\MagicVilla_VillaAPI\\Images\\01.jpg",
                      Rate = 320,
                      Occupancy = 3,
                      Sqft = 140,
                      Amenity = "",
                      CreatedDate = DateTime.Now

                  },
                   new Villa()
                   {
                       Id = 5,
                       Name = "Social Villa",
                       Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                       ImageUrl = "C:\\Users\\nerme\\full-stack-learning-projects\\DotNetMastery_RESTAPIS\\ASP.NET MVC\\MagicVilla\\MagicVilla_VillaAPI\\Images\\01.jpg",
                       Rate = 310,
                       Occupancy = 2,
                       Sqft = 400,
                       Amenity = "",
                       CreatedDate = DateTime.Now

                   },
                    new Villa()
                    {
                        Id = 6,
                        Name = "Ghost Villa",
                        Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                        ImageUrl = "C:\\Users\\nerme\\full-stack-learning-projects\\DotNetMastery_RESTAPIS\\ASP.NET MVC\\MagicVilla\\MagicVilla_VillaAPI\\Images\\01.jpg",
                        Rate = 780,
                        Occupancy = 1,
                        Sqft = 480,
                        Amenity = "",
                        CreatedDate = DateTime.Now
                    },
                     new Villa()
                     {
                         Id = 8,
                         Name = "KOyal Ka Ghosla Villa",
                         Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                         ImageUrl = "C:\\Users\\nerme\\full-stack-learning-projects\\DotNetMastery_RESTAPIS\\ASP.NET MVC\\MagicVilla\\MagicVilla_VillaAPI\\Images\\01.jpg",
                         Rate = 447,
                         Occupancy = 6,
                         Sqft = 700,
                         Amenity = "",
                         CreatedDate = DateTime.Now

                     }

                );

        }
    }
}
