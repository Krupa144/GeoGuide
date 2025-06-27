using GeoTips.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoTips.Repositories
{
    public interface IContinentRepository
    {
        Task<IEnumerable<Continent>> GetAllContinentsAsync();
        Task<Continent> GetContinentByIdAsync(int id);
        Task AddContinentAsync(Continent continent);
        Task UpdateContinentAsync(Continent continent);
        Task DeleteContinentAsync(int id);
        Task<bool> ContinentExistsAsync(int id);
    }
}