using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeoTips.Controllers
{
   
        [Route("api/[controller]")]
        [ApiController]
        public class GeoController : ControllerBase
        {
            private readonly GeoContext _context;

            public GeoController(GeoContext context)
            {
                _context = context;
            }

            [HttpGet("continents")]
            public async Task<IActionResult> GetContinents()
            {
                var continents = await _context.Continents
                    .Select(c => new { c.Id, c.Name })
                    .ToListAsync();

                return Ok(continents);
            }

            [HttpGet("continents/{id}/countries")]
            public async Task<IActionResult> GetCountries(int id)
            {
                var countries = await _context.Countries
                    .Where(c => c.ContinentId == id)
                    .Select(c => new { c.Id, c.Name, c.Tip, c.FlagUrl })
                    .ToListAsync();

                return Ok(countries);
            }
        }
}
