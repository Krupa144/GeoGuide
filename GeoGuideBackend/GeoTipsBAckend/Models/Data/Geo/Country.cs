using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.Text.Json.Serialization;

namespace GeoTipsBackend.Models.Data.Geo
{
    [Table("countries")]
    public class Country : BaseModel
    {
        public Country()
        {
            Name = string.Empty;
            Description = string.Empty;
            Tip = string.Empty;
            FlagUrl = string.Empty;
        }

        [PrimaryKey("id", false)]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Column("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [Column("description")]
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [Column("tip")]
        [JsonPropertyName("tip")]
        public string Tip { get; set; }

        [Column("flag_url")]
        [JsonPropertyName("flag_url")]
        public string FlagUrl { get; set; }

        [Column("continent_id")]
        [JsonPropertyName("continent_id")]
        public int ContinentId { get; set; }
    }
}
