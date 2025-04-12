using AutoMapper;
using GameShelf.Application.DTOs;
using GameShelf.Application.Interfaces;
using GameShelf.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace GameShelf.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher<User> passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserDto> AddAsync(AdminCreateUserDto userDto)
        {
            var exists = await _unitOfWork.UserRepository.EmailExistsAsync(userDto.Email);
            if (exists)
                throw new Exception("Un utilisateur avec cet email existe déjà.");

            var user = _mapper.Map<User>(userDto);
            user.Id = Guid.NewGuid();
            user.PasswordHash = _passwordHasher.HashPassword(user, userDto.Password);

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }

        public async Task DeleteAsync(Guid id)
        {
            User? user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (user is null)
                throw new Exception("Utilisateur introuvable.");

            _unitOfWork.UserRepository.Remove(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            List<User> users = await _unitOfWork.UserRepository.GetAllAsync();
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto?> GetByIdAsync(Guid id)
        {
            User? user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            return user is null ? null : _mapper.Map<UserDto>(user);
        }
    }
}