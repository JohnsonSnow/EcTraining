using Application.Messaging;
using Domain.Repositories;
using SharedKernel;

namespace Application.Features.Course.Queries
{
    internal sealed class GetAllCourseQueryHandler : IQueryHandler<GetAllCoursesQuery, IEnumerable<CourseResponse>>
    {
        private readonly ICourseRepository _courseRepository;

        public GetAllCourseQueryHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<Result<IEnumerable<CourseResponse>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            var courses = await _courseRepository.GetAllCourseAsync();

            List<CourseResponse> coursesResponse = new List<CourseResponse>();
            foreach (var item in courses)
            {
                var course = new CourseResponse(item.Id.ToString(), item.Name, item.Description);

                coursesResponse.Add(course);
            }

            return coursesResponse;
        }
    }
}
