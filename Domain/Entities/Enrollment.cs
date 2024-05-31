using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public sealed class Enrollment : Entity
    {
        private Enrollment(Guid id, Guid studentId, Guid courseId, DateTime startDate, DateTime endDate) : base(id)
        {
            StudentId = studentId;
            CourseId = courseId;
            StartDate = startDate;
            EndDate = endDate;
        }

        private Enrollment()
        {

        }

        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }

        public static Enrollment Create(Guid studentId, Guid courseId, DateTime startDate, DateTime endDate)
        {
            var enrollement = new Enrollment(Guid.NewGuid(), studentId, courseId, startDate, endDate);

            return enrollement;
        }
    }
}
