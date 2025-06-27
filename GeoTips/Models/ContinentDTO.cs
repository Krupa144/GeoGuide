using System.ComponentModel.DataAnnotations;

namespace GeoTips.Models 
{
    public class ContinentDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Continent name is required.")]
        public required string Name { get; set; } 
    }
}