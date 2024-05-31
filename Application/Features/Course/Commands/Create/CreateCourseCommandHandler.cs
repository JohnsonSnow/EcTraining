using Application.Abstractions.Data;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Course.Commands.Create
{
    internal sealed class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCourseCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
        {
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = Domain.Entities.Course.Create(request.name, request.description);

            _courseRepository.Add(course);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
