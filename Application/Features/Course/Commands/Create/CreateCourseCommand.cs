using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Course.Commands.Create
{
    public sealed record CreateCourseCommand(string name, string description) : IRequest
    {
    }
}
