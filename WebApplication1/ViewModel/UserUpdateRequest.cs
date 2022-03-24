using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModel
{
    public class UserUpdateRequest
    {
        [Required]
        [StringLength(128)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(128)]
        public string LastName { get; set; }

        [Required]
        [StringLength(128)]
        public string Email { get; set; }

        [Required]
        public int Age { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
