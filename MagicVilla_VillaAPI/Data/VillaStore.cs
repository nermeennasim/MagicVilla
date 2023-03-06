using MagicVilla_VillaAPI.Models.DTOs;

namespace MagicVilla_VillaAPI.Data
{
    public static class VillaStore
    {
        private static List<VillaDTO> villaList = new()
        {
                new VillaDTO() { Id = 1, Name = "Pool Villa",Occupancy=3,sqft=120 },
                new VillaDTO() { Id = 2, Name = "Beach Villa", Occupancy = 1, sqft = 60 },
                new VillaDTO() { Id = 3, Name = "Mountain Villa", Occupancy = 4, sqft = 100 }

        };

        public static List<VillaDTO> VillaList { get => villaList; set => villaList = value; }
    }
}
