using GeoTipsBackend.Models.Data.Lessons;
using GeoTipsBackend.Repositories.Interfaces;
using GeoTipsBackend.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoTipsBackend.Services.Implementations
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;

        public LessonService(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public Task<IEnumerable<Lesson>> GetAllLessonsAsync()
        {
            return _lessonRepository.GetAllLessonsAsync();
        }

        public Task<Lesson?> GetLessonByIdAsync(int id)
        {
            return _lessonRepository.GetLessonByIdAsync(id);
        }

        public Task AddLessonAsync(Lesson lesson)
        {
            return _lessonRepository.AddLessonAsync(lesson);
        }

        public Task UpdateLessonAsync(Lesson lesson)
        {
            return _lessonRepository.UpdateLessonAsync(lesson);
        }

        public Task DeleteLessonAsync(int id)
        {
            return _lessonRepository.DeleteLessonAsync(id);
        }
    }
}
