using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Data;

namespace WebApplication1.Validation
{
    public class BaseValidator<T> : AbstractValidator<T>
    {
        private readonly IUserRepository repository;
        public BaseValidator(IUserRepository repository)
        {
            this.repository = repository;
        }
        public async Task<bool> IsUnique(string email, CancellationToken arg2)
        {
            var emails = await repository.GetAllEmailAsync();
            if (emails.All(e => e.Equals(email)))
                return true;
            else 
                return false;
        }
    }
}
