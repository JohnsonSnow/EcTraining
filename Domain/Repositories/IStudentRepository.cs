using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllStudentAsync();

        Task<Guid> Add(Student student);
        void Update(Student student);
        void Delete(Student student);

    }
}
