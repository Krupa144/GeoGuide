using GeoTips.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Supabase;
using System.Linq;

namespace GeoTips.Repositories
{
    public class ContinentRepository : IContinentRepository
    {
        private readonly Supabase.Client _supabaseClient;

        public ContinentRepository(Supabase.Client supabaseClient) 
        {
            _supabaseClient = supabaseClient;
        }

        public async Task<IEnumerable<Continent>> GetAllContinentsAsync()
        {
            var response = await _supabaseClient.From<Continent>().Get();
            return response.Models;
        }

        public async Task<Continent> GetContinentByIdAsync(int id)
        {
            var response = await _supabaseClient.From<Continent>()
                                                .Where(c => c.Id == id) 
                                                .Get();
            return response.Models.FirstOrDefault();
        }

        public async Task AddContinentAsync(Continent continent)
        {
            var response = await _supabaseClient.From<Continent>().Insert(continent);
            if (response.Models.Any())
            {
                continent.Id = response.Models.First().Id;
            }
        }

        public async Task UpdateContinentAsync(Continent continent)
        {
            await _supabaseClient.From<Continent>()
                                 .Where(c => c.Id == continent.Id)
                                 .Update(continent);
        }

        public async Task DeleteContinentAsync(int id)
        {
            await _supabaseClient.From<Continent>()
                                 .Where(c => c.Id == id)
                                 .Delete();
        }

        public async Task<bool> ContinentExistsAsync(int id)
        {
            var response = await _supabaseClient.From<Continent>()
                                                .Where(c => c.Id == id)
                                                .Count(Supabase.Postgrest.Constants.CountType.Exact); 
            return response > 0;
        }
    }
}