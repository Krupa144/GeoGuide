using GeoTipsBackend.Models.Data.Lessons;
using GeoTipsBackend.Repositories.Interfaces;
using Supabase;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoTipsBackend.Repositories.Implementations
{
    public class LessonRepository : ILessonRepository
    {
        private readonly Client _supabase;

        public LessonRepository(Client supabase)
        {
            _supabase = supabase;
        }

        public async Task<IEnumerable<Lesson>> GetAllLessonsAsync()
        {
            var response = await _supabase.From<Lesson>().Get();
            return response.Models;
        }

        public async Task<Lesson?> GetLessonByIdAsync(int id)
        {
            var response = await _supabase.From<Lesson>()
                .Where(l => l.Id == id)
                .Get();

            return response.Models.FirstOrDefault();
        }

        public async Task AddLessonAsync(Lesson lesson)
        {
            await _supabase.From<Lesson>().Insert(lesson);
        }

        public async Task UpdateLessonAsync(Lesson lesson)
        {
            await _supabase.From<Lesson>()
                .Where(l => l.Id == lesson.Id)
                .Update(lesson);
        }

        public async Task DeleteLessonAsync(int id)
        {
            await _supabase.From<Lesson>()
                .Where(l => l.Id == id)
                .Delete();
        }
    }
}
