using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Supabase;
using GeoTipsBackend.Models.Data.Geo;
using GeoTipsBackend.Models.Dtos.Geo;
using GeoTipsBackend.Models.Data.Auth;

using static Supabase.Postgrest.Constants.Ordering;

namespace GeoTipsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoController : ControllerBase
    {
        private readonly Supabase.Client _supabase;

        public GeoController(Supabase.Client supabaseClient)
        {
            _supabase = supabaseClient;
        }

        [HttpGet("continents")]
        public async Task<IActionResult> GetAllContinents()
        {
            var response = await _supabase.From<Continent>().Get();
            var continents = response.Models?.Select(c => new ContinentDto
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            return Ok(continents ?? new List<ContinentDto>());
        }

        [HttpGet("continents/{id}")]
        public async Task<IActionResult> GetContinent(int id)
        {
            var response = await _supabase.From<Continent>().Where(x => x.Id == id).Get();
            var continent = response.Models?.FirstOrDefault();
            if (continent == null)
                return NotFound();

            var dto = new ContinentDto
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
            var countries = response.Models?.Select(c => new CountryDto
            {
                Id = c.Id,
                Name = c.Name,
                Tip = c.Tip,
                FlagUrl = c.FlagUrl,
                ContinentId = c.ContinentId 
            }).ToList();
            return Ok(countries ?? new List<CountryDto>()); 
        }

        [HttpGet("countries/{id}")]
        public async Task<IActionResult> GetCountry(int id)
        {
            var response = await _supabase.From<Country>().Where(x => x.Id == id).Get();
            var country = response.Models?.FirstOrDefault();
            if (country == null)
                return NotFound();

            var dto = new CountryDto
            {
                Id = country.Id,
                Name = country.Name,
                ContinentId = country.ContinentId,
                Tip = country.Tip,
                FlagUrl = country.FlagUrl
            };

            return Ok(dto);
        }
    }
}