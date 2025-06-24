using GeoTips.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeoTips.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoController : ControllerBase
    {
        private readonly Supabase.Client _supabase;

        public GeoController()
        {
            _supabase = new Supabase.Client("https://zitxawcefyuhfhqnybyg.supabase.co", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InppdHhhd2NlZnl1aGZocW55YnlnIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NTA3ODg3NzAsImV4cCI6MjA2NjM2NDc3MH0.UCZVX7p38R71IIyR8Nl_HY-w5USCl1wg-QhpCiwLgb8");
            _supabase.InitializeAsync().Wait();
        }

        //[HttpGet("continents")]
        //public async Task<IActionResult> GetContinents()
        //{
        //    var continents = await _context.Continents
        //        .Select(c => new { c.Id, c.Name })
        //        .ToListAsync();

        //    return Ok(continents);
        //}

        //[HttpGet("continents/{id}/countries")]
        //public async Task<IActionResult> GetCountries(int id)
        //{
        //    var countries = await _context.Countries
        //        .Where(c => c.ContinentId == id)
        //        .Select(c => new { c.Id, c.Name, c.Tip, c.FlagUrl })
        //        .ToListAsync();

        //    return Ok(countries);
        //}
        //[HttpPost("continents")]
        //public async Task<IActionResult> AddContinent([FromBody] Continent continent)
        //{
        //    if (continent == null || string.IsNullOrEmpty(continent.Name))
        //    {
        //        return BadRequest("Invalid continent data.");
        //    }
        //    _context.Continents.Add(continent);
        //    await _context.SaveChangesAsync();
        //    return CreatedAtAction(nameof(GetContinents), new { id = continent.Id }, continent);
        //}
        [HttpGet("continents")]
        public async Task<IActionResult> GetAllContinents()
        {
            var response = await _supabase.From<Continent>().Get();
            var continents = response.Models.Select(c => new ContinentDTO
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
            return Ok(continents);
        }

        [HttpGet("continents/{id}")]
        public async Task<IActionResult> GetContinent(int id)
        {
            var response = await _supabase.From<Continent>().Where(x => x.Id == id).Get();

            var continent = response.Models.FirstOrDefault();
            if (continent == null)
                return NotFound();

            var dto = new ContinentDTO
            {
                Id = continent.Id,
                Name = continent.Name
            };

            return Ok(dto);
        }

        [HttpGet("countries")]
        public async Task<IActionResult> GetAllCountries()
        {
            var response = await _supabase.From<Country>().Get();
            var countries = response.Models.Select(c => new CountryDTO
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
            return Ok(countries);
        }

        [HttpGet("countries/{id}")]
        public async Task<IActionResult> GetCountry(int id)
        {
            var response = await _supabase.From<Country>().Where(x => x.Id == id).Get();

            var country = response.Models.FirstOrDefault();
            if (country == null)
                return NotFound();

            var dto = new CountryDTO
            {
                Id = country.Id,
                Name = country.Name,
                ContinentId = country.ContinentId,
                Tip = country.Tip
            };

            return Ok(dto);
        }
    }
}
