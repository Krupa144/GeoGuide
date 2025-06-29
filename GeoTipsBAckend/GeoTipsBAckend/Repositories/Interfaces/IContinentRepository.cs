using GeoTipsBackend.Models.Data.Geo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoTipsBackend.Repositories.Interfaces
{
    public interface IContinentRepository
    {
        Task<IEnumerable<Continent>> GetAllContinentsAsync();
        Task<Continent?> GetContinentByIdAsync(int id); 
        Task AddContinentAsync(Continent continent);
        Task UpdateContinentAsync(Continent continent);
        Task DeleteContinentAsync(int id);
        Task<bool> ContinentExistsAsync(int id);
    }
}