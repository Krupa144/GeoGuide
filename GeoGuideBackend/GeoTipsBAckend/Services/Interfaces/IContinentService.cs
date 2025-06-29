using System.Collections.Generic;
using System.Threading.Tasks;
using GeoTipsBackend.Models.Dtos.Geo; 

namespace GeoTipsBackend.Services.Interfaces 
{
    public interface IContinentService
    {
        Task<IEnumerable<ContinentDto>> GetAllContinentsAsync();
        Task<ContinentDto?> GetContinentByIdAsync(int id); 
        Task<ContinentDto> AddContinentAsync(ContinentDto continentDto);
        Task<bool> UpdateContinentAsync(int id, ContinentDto continentDto);
        Task<bool> DeleteContinentAsync(int id);
    }
}