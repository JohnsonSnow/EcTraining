using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public sealed class Student : Entity
    {
        private Student(Guid id, string fullname, string emailAddress) : base(id)
        {
            FullName = fullname;
            EmailAddress = emailAddress;
        }

        private Student() { }

        public string FullName { get; set; }
        public string EmailAddress { get; set; }

        public static Student Create(string fullname, string emailAddress)
        {
            var student = new Student(Guid.NewGuid(), fullname, emailAddress);

            return student;
        }
    }
}
