using GeoTipsBackend.Models.Data.Lessons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoTipsBackend.Services.Interfaces
{
    public interface ILessonService
    {
        Task<IEnumerable<Lesson>> GetAllLessonsAsync();
        Task<Lesson?> GetLessonByIdAsync(int id);
        Task AddLessonAsync(Lesson lesson);
        Task UpdateLessonAsync(Lesson lesson);
        Task DeleteLessonAsync(int id);
    }
}
