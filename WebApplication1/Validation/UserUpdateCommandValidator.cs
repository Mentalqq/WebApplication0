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
                .MustAsync((u, p, c) => IsUnique(u.Id, u.Email)).WithMessage("Email already exist (Id + Email)");
        }

        public async Task<bool> IsUnique(long id, string email)
        {
            var existUser = await repository.GetUserByEmailAsync(email);

            return existUser == null || existUser.Id == id;
        }
    }
}