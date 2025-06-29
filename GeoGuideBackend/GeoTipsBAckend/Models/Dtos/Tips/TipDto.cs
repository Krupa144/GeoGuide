namespace GeoTipsBackend.Models.Dtos.Tips
{
    public class TipDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int LessonId { get; set; } 
    }
}