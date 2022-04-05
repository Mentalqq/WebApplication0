using System.Collections.Generic;
using WebApplication1.DTO;

namespace WebApplication1.ViewModel
{
    public class UserGetResponse
    {
        public IEnumerable<UserDto> Users { get; set; }
    }
}
