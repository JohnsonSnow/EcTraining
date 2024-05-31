using Application.Messaging;

namespace Application.Features.Course.Queries
{
    public sealed record GetAllCoursesQuery() : IQuery<IEnumerable<CourseResponse>>
    {
    }
}
