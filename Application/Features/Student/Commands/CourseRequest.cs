using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Student.Commands
{
    public sealed record CourseRequest(Guid CourseId, DateTime StartDate, DateTime EndDate)
    {

    }
}
