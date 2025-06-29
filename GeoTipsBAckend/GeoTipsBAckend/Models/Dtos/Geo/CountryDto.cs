namespace GeoTipsBackend.Models.Dtos.Geo 
{
    public class CountryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ContinentId { get; set; }
        public string Tip { get; set; } = string.Empty;   
        public string FlagUrl { get; set; } = string.Empty;
    }
}