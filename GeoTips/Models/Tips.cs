using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations; 

namespace GeoTips.Models
{
    [Table("Tips")] 
    public class Tip : BaseModel
    {
        [PrimaryKey("Id", false)]
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [Column("TipContent")] 
        [JsonPropertyName("TipContent")]
        public string TipContent { get; set; } 

    }

    public class TipDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Tip content is required.")] 
        public required string Tip { get; set; } 
    }
}