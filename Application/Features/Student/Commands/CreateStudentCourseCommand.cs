﻿using Domain.Entities;
using MediatR;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Student.Commands
{
    public record CreateStudentCourseCommand(string fullname, string emailAdress, List<CourseRequest> enrollments) : IRequest<Result>;

}
