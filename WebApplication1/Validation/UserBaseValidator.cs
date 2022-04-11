using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.DTO;

namespace WebApplication1.Validation
{
    public class UserBaseValidator<T> : AbstractValidator<T> where T : IUserValidationModel
    {
        private readonly IUserRepository repository;

        public UserBaseValidator(IUserRepository repository)
        {
            this.repository = repository;

            Validation();
        }

        public async Task<bool> IsUnique(string email, CancellationToken arg2)
        {
            string existEmail = await repository.GetEmailAsync(email);
            if (existEmail is null)
                return true;
            else 
                return false;
        }

        public void Validation()
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
