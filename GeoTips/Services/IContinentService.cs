using GeoTips.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoTips.Services
{
    public interface IContinentService
    {
        Task<IEnumerable<ContinentDTO>> GetAllContinentsAsync();
        Task<ContinentDTO> GetContinentByIdAsync(int id);
        Task<ContinentDTO> AddContinentAsync(ContinentDTO continentDto);
        Task<bool> UpdateContinentAsync(int id, ContinentDTO continentDto);
        Task<bool> DeleteContinentAsync(int id);
    }
}