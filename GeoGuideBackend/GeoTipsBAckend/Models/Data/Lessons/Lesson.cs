using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.Text.Json.Serialization;

namespace GeoTipsBackend.Models.Data.Lessons
{
    [Table("lessons")]
    public class Lesson : BaseModel
    {
        public Lesson()
        {
            Title = string.Empty;
            Content = string.Empty;
            ImageUrl = string.Empty;
        }

        [PrimaryKey("id", false)]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Column("title")]
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [Column("content")]
        [JsonPropertyName("content")]
        public string Content { get; set; }

        [Column("image_url")]
        [JsonPropertyName("image_url")]
        public string ImageUrl { get; set; }
    }
}
