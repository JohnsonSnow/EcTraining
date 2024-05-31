using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public sealed class Course : Entity
    {
        private Course(Guid id, string name, string description) : base(id)
        {
            Name = name;
            Description = description;
        }

        private Course()
        {

        }

        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;


        public static Course Create(string name, string description)
        {
            var course = new Course(Guid.NewGuid(), name, description);

            return course;
        }
    }
}
