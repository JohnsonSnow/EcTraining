using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    internal sealed class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public CourseRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void Add(Course course)
        {
            _dbcontext.Courses.AddAsync(course);
        }

        public void Delete(Course course)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Course>> GetAllCourseAsync()
        {
            return await _dbcontext.Courses.ToListAsync();
        }

        public void Update(Course course)
        {
            throw new NotImplementedException();
        }
    }
}
