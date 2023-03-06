using MagicVilla_VillaAPI.Models.DTOs;

namespace MagicVilla_VillaAPI.Data
{
    public static class VillaStore
    {
        private static List<VillaDTO> villaList = new List<VillaDTO>()
        {
                new VillaDTO() { Id = 1, Name = "Pool Villa" },
                new VillaDTO() { Id = 2, Name = "Beach Villa" },
                new VillaDTO() { Id = 3, Name = "Mountain Villa" }

        };

        public static List<VillaDTO> VillaList { get => villaList; set => villaList = value; }
    }
}
