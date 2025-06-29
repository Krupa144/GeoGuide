using GeoTipsBackend.Models.Data.Geo;
using GeoTipsBackend.Repositories.Interfaces;
using Supabase;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Supabase.Postgrest.Constants.CountType;

namespace GeoTipsBackend.Repositories.Implementations
{
    public class ContinentRepository : IContinentRepository
    {
        private readonly Supabase.Client _supabase;

        public ContinentRepository(Supabase.Client supabaseClient)
        {
            _supabase = supabaseClient;
        }

        public async Task<Continent?> GetContinentByIdAsync(int id)
        {
            var response = await _supabase
                .From<Continent>()
                .Where(c => c.Id == id)
                .Get();

            return response.Models?.FirstOrDefault();
        }

        public async Task AddContinentAsync(Continent continent)
        {
            await _supabase
                .From<Continent>()
                .Insert(continent);
        }

        public async Task UpdateContinentAsync(Continent continent)
        {
            await _supabase
                .From<Continent>()
                .Where(c => c.Id == continent.Id)
                .Update(continent);
        }

        public async Task DeleteContinentAsync(int id)
        {
            await _supabase
                .From<Continent>()
                .Where(c => c.Id == id)
                .Delete();
        }

        public async Task<bool> ContinentExistsAsync(int id)
        {
            var count = await _supabase
                .From<Continent>()
                .Where(c => c.Id == id)
                .Count(Exact);  

            return count > 0;
        }

        public async Task<IEnumerable<Continent>> GetAllContinentsAsync()
        {
            var response = await _supabase
                .From<Continent>()
                .Order("name", Supabase.Postgrest.Constants.Ordering.Ascending)
                .Get();

            return response.Models ?? Enumerable.Empty<Continent>();
        }
    }
}
