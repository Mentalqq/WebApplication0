using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Domain
{
    public class User
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public Guid UserKey { get; set; }

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

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
