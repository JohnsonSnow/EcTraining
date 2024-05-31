using Application.Features.Course.Commands.Create;
using Application.Features.Student.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    public sealed class StudentController : ApiController
    {
        public StudentController(ISender sender) : base(sender)
        {
        }

        [HttpPost(nameof(StudentCourseEnrollment))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> StudentCourseEnrollment([FromBody] CreateStudentCourseCommand request, CancellationToken cancellationToken)
        {
            var command = new CreateStudentCourseCommand(request.fullname, request.emailAdress, request.enrollments);
            await Sender.Send(command, cancellationToken);

            return NoContent();

        }
    }
}
