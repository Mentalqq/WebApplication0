using FluentValidation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Application.Commands;
using WebApplication1.Data;

namespace WebApplication1.Validation
{
    public class UserUpdateCommandValidator : UserBaseValidator<UserUpdateCommand>
    {

        public UserUpdateCommandValidator(IUserRepository repository)
            : base(repository)
        {
            RuleFor(u => u.Email)
                .MustAsync(IsUnique).WithMessage("Email already exist");
        }

        public override async Task<bool> IsUnique(string email, CancellationToken arg2)
        {
            string existEmail = await repository.GetEmailAsync(email);
            if (existEmail is null)
                return true;
            else
                return false;
                
        }
    }
}