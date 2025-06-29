using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace GeoTipsBackend.Models.Data.Tips 
{
    [Table("tips")]
    public class Tips : BaseModel
    {
        public Tips()
        {
            Title = string.Empty;
            Content = string.Empty;
        }

        [PrimaryKey("id", false)]
        public int Id { get; set; }

        [Column("title")]
        public required string Title { get; set; }

        [Column("content")]
        public required string Content { get; set; }

        [Column("lesson_id")]
        public int LessonId { get; set; }
    }
}