using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.Text.Json.Serialization; // Pamiętaj o tym using, jeśli używasz [Column]

namespace GeoTipsBackend.Models.Data.Lessons
{
    [Table("progress")] // Upewnij się, że nazwa tabeli jest POPRAWNA (dokładnie jak w Supabase)
    public class Progress : BaseModel
    {
        // WAŻNE: Usunięto 'public int Id { get; set; }'
        // Jeśli Twoja tabela 'progress' w bazie danych NIE MA kolumny 'id',
        // to ta właściwość musi zniknąć z modelu.

        // Klucz złożony: kombinacja UserId i LessonId jest unikalna
        [PrimaryKey("user_id", false)] // "user_id" to nazwa KOLUMNY w bazie danych
        [Column("user_id")]
        public string UserId { get; set; }

        [PrimaryKey("lesson_id", false)] // "lesson_id" to nazwa KOLUMNY w bazie danych
        [Column("lesson_id")]
        public int LessonId { get; set; }

        [Column("is_completed")] // "is_completed" to nazwa KOLUMNY w bazie danych
        public bool IsCompleted { get; set; }
    }
}