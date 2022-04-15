using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModel
{
    public class UserAddRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }
    }
}
