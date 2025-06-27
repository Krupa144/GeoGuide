using GeoTips.Models;
using GeoTips.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoTips.Services
{
    public class ContinentService : IContinentService
    {
        private readonly IContinentRepository _continentRepository;

        public ContinentService(IContinentRepository continentRepository)
        {
            _continentRepository = continentRepository;
        }

        public async Task<IEnumerable<ContinentDTO>> GetAllContinentsAsync()
        {
            var continents = await _continentRepository.GetAllContinentsAsync();
            return continents.Select(c => new ContinentDTO
            {
                Id = c.Id,
                Name = c.Name
            });
        }

        public async Task<ContinentDTO> GetContinentByIdAsync(int id)
        {
            var continent = await _continentRepository.GetContinentByIdAsync(id);
            if (continent == null)
            {
                return null;
            }
            return new ContinentDTO
            {
                Id = continent.Id,
                Name = continent.Name
            };
        }

        public async Task<ContinentDTO> AddContinentAsync(ContinentDTO continentDto)
        {
            var continent = new Continent
            {
                Name = continentDto.Name
            };
            await _continentRepository.AddContinentAsync(continent);
            continentDto.Id = continent.Id;
            return continentDto;
        }

        public async Task<bool> UpdateContinentAsync(int id, ContinentDTO continentDto)
        {
            var existingContinent = await _continentRepository.GetContinentByIdAsync(id);
            if (existingContinent == null)
            {
                return false;
            }

            existingContinent.Name = continentDto.Name;
            await _continentRepository.UpdateContinentAsync(existingContinent);
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