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
        private protected readonly IUserRepository repository;

        public UserBaseValidator(IUserRepository repository)
        {
            this.repository = repository;

            Validation();
        }
        public void Validation()
        {
            RuleFor(u => u.FirstName)
                .NotEmpty().WithMessage("Must be not empty field")
                .Length(1, 128).WithMessage("Must be more than 1 letter and less than 128");
            RuleFor(u => u.LastName)
                .NotEmpty().WithMessage("Must be not empty field")
                .Length(1, 128).WithMessage("Must be more than 1 letter and less than 128");
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Must be not empty field")
                .Length(1, 128).WithMessage("Must be more than 1 letter and less than 128");
            RuleFor(u => u.Age)
                .NotEmpty().WithMessage("Must be not empty field")
                .GreaterThan(0).LessThan(100);
        }
        public virtual async Task<bool> IsUnique(string email)
        {
            var existUser = await repository.GetUserByEmailAsync(email);

            return existUser == null;
        }

        public virtual async Task<bool> IsUnique(long id, string email)
        {
            var existUser = await repository.GetUserByEmailAsync(email);

            return existUser == null || existUser.Id == id;
        }
    }
}
