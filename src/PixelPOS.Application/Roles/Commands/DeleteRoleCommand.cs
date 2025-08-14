using MediatR;

namespace PixelPOS.Application.Roles.Commands
{
    public class DeleteRoleCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
