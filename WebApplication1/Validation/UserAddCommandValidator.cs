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

        }
    }
}
