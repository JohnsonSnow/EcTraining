using Application.Features.Course;
using Application.Features.Course.Commands.Create;
using Application.Features.Course.Queries;

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;
using SharedKernel;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    public sealed class CourseController : ApiController
    {
        public CourseController(ISender sender) : base(sender)
        {
        }

        /// <summary>
        /// Gets all course.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet(nameof(GetAllCourse))]
        [ProducesResponseType(typeof(Result<CourseResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllCourse(CancellationToken cancellationToken)
        {
            var query = new GetAllCoursesQuery();
            var result = await Sender.Send(query, cancellationToken);
            return Ok(result);

        }

        /// <summary>
        /// Creates the course.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost(nameof(CreateCourse))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var command = new CreateCourseCommand(request.name, request.description);
            await Sender.Send(command, cancellationToken);

            return NoContent();

        }
    }
}
