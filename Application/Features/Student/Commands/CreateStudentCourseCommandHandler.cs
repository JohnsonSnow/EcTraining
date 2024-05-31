using Application.Abstractions.Data;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Student.Commands
{
    internal sealed record CreateStudentCourseCommandHandler : IRequestHandler<CreateStudentCourseCommand, Result>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateStudentCourseCommandHandler> _logger;

        public CreateStudentCourseCommandHandler(IStudentRepository studentRepository, IEnrollmentRepository enrollmentRepository, IUnitOfWork unitOfWork, ILogger<CreateStudentCourseCommandHandler> logger)
        {
            _studentRepository = studentRepository;
            _enrollmentRepository = enrollmentRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }


        public async Task<Result> Handle(CreateStudentCourseCommand request, CancellationToken cancellationToken)
        {
            _unitOfWork.BegingTransactionAsync();

            try
            {
                var student = Domain.Entities.Student.Create(request.fullname, request.emailAdress);

                if (student == null)
                {
                    return Result.Failure(Error.NullValue);
                }
                var studentId = await _studentRepository.Add(student);

                foreach (var item in request.enrollments)
                {
                    var enrollment = Domain.Entities.Enrollment.Create(studentId, item.CourseId, item.StartDate, item.EndDate);

                    _enrollmentRepository.Add(enrollment);
                }
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await _unitOfWork.CommitTransactionAsync();

                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message, args: ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
