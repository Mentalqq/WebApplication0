using FluentValidation;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Application.Commands;
using WebApplication1.Data;

namespace WebApplication1.Validation
{
    public class UserAddCommandValidator : UserBaseValidator<UserAddCommand>
    {
        public UserAddCommandValidator(IUserRepository repository)
            : base(repository)
        {
            RuleFor(u => u.Email)
                .MustAsync(IsUnique).WithMessage("Email already exist")
                .NotEmpty().WithMessage("Must be not empty field")
                .Length(1, 128).WithMessage("Must be more than 1 letter and less than 128");


        }
    }
}
