using FluentValidation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Application.Commands;
using WebApplication1.Data;

namespace WebApplication1.Validation
{
    public class UserUpdateCommandValidator : BaseValidator<UserUpdateCommand>
    {
        public UserUpdateCommandValidator(IUserRepository repository)
            : base(repository)
        {
            RuleFor(u => u.FirstName)
                .NotEmpty().WithMessage("Must be not empty field");
            RuleFor(u => u.FirstName)
                .Length(1, 128).WithMessage("Must be more than 1 letter and less than 128");
            RuleFor(u => u.LastName)
                .NotEmpty().WithMessage("Must be not empty field");
            RuleFor(u => u.LastName)
                .Length(1, 128).WithMessage("Must be more than 1 letter and less than 128");
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Must be not empty field");
            RuleFor(u => u.Email)
                .MustAsync(IsUnique).WithMessage("Email already exist");
            RuleFor(u => u.Email)
                .Length(1, 128).WithMessage("Must be more than 1 letter and less than 128");
            RuleFor(u => u.Age)
                .NotEmpty().WithMessage("Must be not empty field");
            RuleFor(u => u.Age).GreaterThan(0).LessThan(100);
        }
    }
}