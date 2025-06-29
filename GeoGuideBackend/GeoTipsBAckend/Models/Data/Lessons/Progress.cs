using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.Text.Json.Serialization;

namespace GeoTipsBackend.Models.Data.Lessons
{
    [Table("progress")]
    public class Progress : BaseModel
    {
        public Progress()
        {
            UserId = string.Empty;
            IsCompleted = false;
        }

        [PrimaryKey("id", false)]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Column("user_id")]
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [Column("lesson_id")]
        [JsonPropertyName("lesson_id")]
        public int LessonId { get; set; }

        [Column("is_completed")]
        [JsonPropertyName("is_completed")]
        public bool IsCompleted { get; set; }
    }
}
