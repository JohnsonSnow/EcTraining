using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    internal sealed class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public StudentRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Guid> Add(Student student)
        {
            await _dbcontext.Students.AddAsync(student);
            return student.Id;
        }

        public void Delete(Student student)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Student>> GetAllStudentAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
