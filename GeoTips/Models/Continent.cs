using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.Diagnostics.Metrics;

namespace GeoTips.Models
{
    [Table("Continents")]
    public class Continent : BaseModel
    {
        [PrimaryKey("Id")]
        public int Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }
    }
}
