using AutoMapper;
using CURE.Application.DTOs.Users;
using CURE.Application.Exceptions;
using CURE.Application.Interfaces.Repositories;
using CURE.Application.Interfaces.Security;
using CURE.Application.Interfaces.Services;
using CURE.Domain.Entities;

namespace CURE.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHash _passwordHasher;
        public UserService(IUserRepository userRepository, IMapper mapper, IPasswordHash passwordHash)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHash;
        }

        public async Task<long> CreateAsync(CreateUserDto dto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
                throw new BadRequestException("User with this email already exists");

            var user = _mapper.Map<Users>(dto);
            user.IsActive = true;
            user.CreatedAt = DateTime.UtcNow;
            user.PasswordHash = _passwordHasher.Hash(dto.Password);
            user.Roles.Add(await _userRepository.GetRoleByNameAsync(dto.userRole));

            user.Person = new Person
            {
                FullName = dto.FullName,
                Gender = dto.Gender,
                City = dto.City,
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address,
                DateOfBirth = dto.DateOfBirth

            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
            return user.Id;
        }

        public async Task<List<UserResponseDto>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            var users = await _userRepository.GetAllAsync(pageNumber, pageSize);
            return _mapper.Map<List<UserResponseDto>>(users);
        }

        public async Task<UserResponseDto?> GetByEmailAsync(string email, bool trackChanges = false)
        {
            var user = await _userRepository.GetByEmailAsync(email, trackChanges);
            if (user == null)
                throw new NotFoundException("User not found");
            return _mapper.Map<UserResponseDto?>(user);
        }

        public async Task<UserResponseDto?> GetByIdAsync(long id, bool trackChanges = false)
        {
            var user = await _userRepository.GetByIdAsync(id, trackChanges);
            if (user == null)
                throw new NotFoundException("User not found");
            return _mapper.Map<UserResponseDto?>(user);
        }

        public async Task SoftDelete(long id)
        {
            var user = await _userRepository.GetByIdAsync(id, trackChanges: true);

            if (user == null)
                throw new NotFoundException("User not found");

            _userRepository.SoftDelete(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(long id, UpdateUserDto dto)
        {
            var user = await _userRepository.GetByIdAsync(id, trackChanges: true);

            if (user == null)
                throw new NotFoundException("User not found");

            _mapper.Map(dto, user);

            if (user.Person != null)
            {
                user.Person.FullName = dto.FullName;
                user.Person.Gender = dto.Gender;
                user.Person.PhoneNumber = dto.PhoneNumber;
                user.Person.Address = dto.Address;
                user.Person.City = dto.City;
                user.Person.DateOfBirth = dto.DateOfBirth;
            }

            user.UpdatedAt = DateTime.UtcNow;

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

        }


    }
}
