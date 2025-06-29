using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoTipsBackend.Models.Data.Geo; 
using GeoTipsBackend.Models.Dtos.Geo;
using GeoTipsBackend.Repositories.Implementations; 
using GeoTipsBackend.Repositories.Interfaces;
using GeoTipsBackend.Services.Interfaces;

namespace GeoTipsBackend.Services.Implementations 
{
    public class ContinentService : IContinentService
    {
        private readonly IContinentRepository _continentRepository;

        public ContinentService(IContinentRepository continentRepository)
        {
            _continentRepository = continentRepository;
        }

        public async Task<IEnumerable<ContinentDto>> GetAllContinentsAsync()
        {
            var continents = await _continentRepository.GetAllContinentsAsync();
            return continents.Select(c => new ContinentDto { Id = c.Id, Name = c.Name }).ToList();
        }

        public async Task<ContinentDto?> GetContinentByIdAsync(int id) 
        {
            var continent = await _continentRepository.GetContinentByIdAsync(id);
            if (continent == null)
            {
                return null;
            }
            return new ContinentDto { Id = continent.Id, Name = continent.Name };
        }

        public async Task<ContinentDto> AddContinentAsync(ContinentDto continentDto)
        {
            var continent = new Continent { Name = continentDto.Name };
            await _continentRepository.AddContinentAsync(continent);
            continentDto.Id = continent.Id; 
            return continentDto;
        }

        public async Task<bool> UpdateContinentAsync(int id, ContinentDto continentDto)
        {
            var continent = await _continentRepository.GetContinentByIdAsync(id);
            if (continent == null)
            {
                return false;
            }
            continent.Name = continentDto.Name;
            await _continentRepository.UpdateContinentAsync(continent);
            return true;
        }

        public async Task<bool> DeleteContinentAsync(int id)
        {
            var exists = await _continentRepository.ContinentExistsAsync(id);
            if (!exists)
            {
                return false;
            }
            await _continentRepository.DeleteContinentAsync(id);
            return true;
        }
    }
}