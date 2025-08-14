using MediatR;
using PixelPOS.Application.Users.Commands;
using PixelPOS.Application.Users.DTOs;
using PixelPOS.Domain.Entities;
using PixelPOS.Domain.Repositories;
using PixelPOS.Domain.Security;
using System.Security.Cryptography;
using System.Text;

namespace PixelPOS.Application.Users.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IPasswordHasher _passwordHasher;

        public CreateUserHandler(IUserRepository userRepository, ICompanyRepository companyRepository, IRoleRepository roleRepository, IPasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _companyRepository = companyRepository;
            _roleRepository = roleRepository;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(request.CompanyId, cancellationToken);
            if (company == null || !company.IsActive)
                throw new Exception("Company not found or inactive.");

            if (await _userRepository.ExistsByUsernameAsync(request.CompanyId, request.Username, null, cancellationToken))
                throw new Exception("A user with the same username already exists in this company.");

            var roles = new List<Role>();
            foreach (var roleId in request.RoleIds.Distinct())
            {
                var role = await _roleRepository.GetByIdAsync(roleId, cancellationToken);
                if (role == null)
                    throw new Exception($"Role with id {roleId} not found.");
                roles.Add(role);
            }

            var passwordHash = _passwordHasher.Hash(request.Password);

            var user = new User
            {
                CompanyId = request.CompanyId,
                BranchId = request.BranchId,
                Username = request.Username,
                Email = request.Email,
                FullName = request.FullName,
                PasswordHash = passwordHash,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UserRoles = roles.Select(r => new UserRole { RoleId = r.Id }).ToList()
            };

            await _userRepository.AddAsync(user, cancellationToken);
            await _userRepository.SaveChangesAsync(cancellationToken);

            return new UserDto
            {
                Id = user.Id,
                CompanyId = user.CompanyId,
                BranchId = user.BranchId,
                Username = user.Username,
                Email = user.Email,
                FullName = user.FullName,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                Roles = roles.Select(r => r.Name).ToList()
            };
        }
    }
}
