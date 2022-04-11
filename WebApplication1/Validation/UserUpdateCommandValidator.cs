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
            
        }
    }
}