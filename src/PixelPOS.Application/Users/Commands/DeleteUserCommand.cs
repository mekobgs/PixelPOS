using MediatR;

namespace PixelPOS.Application.Users.Commands
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
