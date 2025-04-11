using GameShelf.Application.DTOs;
using GameShelf.Application.Interfaces;
using GameShelf.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace GameShelf.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtService _jwtService;
        public AuthService(IUnitOfWork unitOfWork, IPasswordHasher<User> passwordHasher, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<AuthResultDto> LoginAsync(LoginRequestDto dto)
        {
            var user = await _unitOfWork.UserRepository.GetByEmailAsync(dto.Email);
            if (user == null)
                throw new Exception("Utilisateur non trouvé.");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
                throw new Exception("Mot de passe incorrect.");

            var token = _jwtService.GenerateToken(user);

            return new AuthResultDto
            {
                UserId = user.Id,
                Email = user.Email,
                Token = token
            };
        }

        public async Task<AuthResultDto> RegisterAsync(RegisterRequestDto dto)
        {
            if (await _unitOfWork.UserRepository.EmailExistsAsync(dto.Email))
                throw new Exception("Email déjà utilisé.");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                Pseudo = dto.Pseudo,
                PasswordHash = "", // hashé juste après
                Role = "User"
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);
            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync(); // ✅ Appel unique ici

            var token = _jwtService.GenerateToken(user);

            return new AuthResultDto
            {
                UserId = user.Id,
                Email = user.Email,
                Token = token
            };
        }
    }
}