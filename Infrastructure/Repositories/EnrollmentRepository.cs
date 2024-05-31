using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    internal sealed class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public EnrollmentRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void Add(Enrollment enrollment)
        {
            _dbcontext.Enrollments.Add(enrollment);
        }
    }
}
