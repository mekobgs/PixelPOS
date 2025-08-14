using MediatR;
using PixelPOS.Application.Users.DTOs;

namespace PixelPOS.Application.Users.Queries
{
    public class GetUserByIdQuery : IRequest<UserDto?>
    {
        public int Id { get; set; }
    }
}
