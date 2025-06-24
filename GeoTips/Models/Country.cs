using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace GeoTips.Models
{
    [Table("Countries")]
    public class Country : BaseModel
    {
        [PrimaryKey("Id")]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("ContinentId")]
        public int ContinentId { get; set; }

        [Column("Tip")]
        public string Tip { get; set; }

    }
}
