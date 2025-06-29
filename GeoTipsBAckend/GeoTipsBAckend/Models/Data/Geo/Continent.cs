using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.Text.Json.Serialization;

namespace GeoTipsBackend.Models.Data.Geo
{
    [Table("continents")]
    public class Continent : BaseModel
    {
        public Continent()
        {
            Name = string.Empty;
        }

        [PrimaryKey("id", false)]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Column("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
