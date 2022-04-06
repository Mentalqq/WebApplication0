using FluentValidation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Application.Commands;
using WebApplication1.Data;

namespace WebApplication1.Validation
{
    public class UserUpdateCommandValidator : AbstractValidator<UserUpdateCommand>
    {
        private readonly IUserRepository repository;
        public UserUpdateCommandValidator(IUserRepository repository)
        {
            this.repository = repository;

            RuleFor(au => au.FirstName)
                .NotEmpty().WithMessage("Must be not empty field");
            RuleFor(au => au.FirstName)
                .Length(1, 128).WithMessage("Must be more than 1 letter and less than 128");
            RuleFor(au => au.LastName)
                .NotEmpty().WithMessage("Must be not empty field");
            RuleFor(au => au.LastName)
                .Length(1, 128).WithMessage("Must be more than 1 letter and less than 128");
            RuleFor(au => au.Email)
                .NotEmpty().WithMessage("Must be not empty field");
            RuleFor(au => au.Email)
                .MustAsync(isUnique).WithMessage("Email already exist");
            RuleFor(au => au.Email)
                .Length(1, 128).WithMessage("Must be more than 1 letter and less than 128");
            RuleFor(au => au.Age)
                .NotEmpty().WithMessage("Must be not empty field");
            RuleFor(au => au.Age).GreaterThan(0).LessThan(100);
        }
        private async Task<bool> isUnique(string email, CancellationToken arg2)
        {
            var users = await repository.GetAllAsync();
            return users.All(ue => ue.Email.Equals(email));
        }
    }
}