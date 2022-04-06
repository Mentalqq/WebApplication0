using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTO
{
    public class UserDto
    {
        public long Id { get; set; }

        public Guid UserKey { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
