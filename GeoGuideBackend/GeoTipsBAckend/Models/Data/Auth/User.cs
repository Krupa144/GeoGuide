using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;

namespace GeoTipsBackend.Models.Data.Auth 
{
    [Table("users")]
    public class User : BaseModel
    {
        public User() { Email = string.Empty; Id = string.Empty; } 
        [PrimaryKey("id", false)] public string Id { get; set; } 
        [Column("email")] public required string Email { get; set; }
        [Column("created_at")] public DateTime CreatedAt { get; set; }
    }
}