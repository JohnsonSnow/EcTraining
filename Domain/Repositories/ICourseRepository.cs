using Domain.Entities;

namespace Domain.Repositories
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllCourseAsync();

        void Add(Course course);
        void Update(Course course);
        void Delete(Course course);
    }
}
