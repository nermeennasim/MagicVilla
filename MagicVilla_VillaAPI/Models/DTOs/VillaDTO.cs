using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAPI.Models.DTOs
{
    public class VillaDTO
    {
        public int Id { get; set; }
        //validation
        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }

    }
}
