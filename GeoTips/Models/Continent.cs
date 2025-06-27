using Supabase.Postgrest.Attributes; 
using Supabase.Postgrest.Models;    
using System.Text.Json.Serialization; 

namespace GeoTips.Models
{
    [Table("Continents")] 
    public class Continent : BaseModel
    {
        [PrimaryKey("Id", false)] 
        [JsonPropertyName("Id")] 
        public int Id { get; set; }

        [Column("Name")]
        [JsonPropertyName("Name")] 
        public string Name { get; set; } 
    }
}