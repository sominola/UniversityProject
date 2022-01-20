using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using UniversityProject.Data.Entities;
using UniversityProject.Data.Repositories.Interfaces;
using UniversityProject.Domain.Dto.User;
using UniversityProject.Domain.Exceptions;
using UniversityProject.Domain.Extensions;
using UniversityProject.Domain.Services.Interfaces;

namespace UniversityProject.Domain.Services;

public class UserService: IUserService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IHttpContextAccessor _context;
    public UserService(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor context)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _context = context;
        _passwordHasher = new PasswordHasher<User>();
    }
    
    public async Task CreateUserAsync(RegisterUserDto model)
    {
        var emailTaken = await IsEmailTakenAsync(model.Email);
        if (emailTaken)
        {
            throw new PageResultException("Account already exists", HttpStatusCode.Conflict);
        }
        
        try
        {
            var user = _mapper.Map<User>(model);
            var userRole = (await _unitOfWork.RoleRepository.GetAllAsync()).FirstOrDefault();
            user.HashedPassword = _passwordHasher.HashPassword(user, model.Password);
            user.Roles = new List<Role> {userRole};
            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveAsync();

        }
        catch
        { 
            
            throw new PageResultException( "Cannot create account.",HttpStatusCode.InternalServerError);
        }
    }

    public async Task<UpdateUserDto> GetCurrentUserAsync()
    {
        var id = _context.HttpContext.User.GetCurrentUserId();
        var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
        var roles = await _unitOfWork.RoleRepository.GetRolesByUserAsync(id);
        user.Roles = roles;
        return _mapper.Map<UpdateUserDto>(user);
    }
    
    
    public async Task UpdateCredentialsAsync(UpdateUserDto model)
    {
        
        var userId = _context.HttpContext.User.GetCurrentUserId();
        var isSameEmail = await _unitOfWork.UserRepository.GetEmailByAsync(userId) == model.Email;
            
        if (!isSameEmail && await IsEmailTakenAsync(model.Email))
            throw new PageResultException("This email is taken", HttpStatusCode.Conflict);
        
        var user = _mapper.Map<User>(model);
        user.Id = userId;
        await _unitOfWork.UserRepository.UpdateCredentialsAsync(user);
        await _unitOfWork.SaveAsync();
    }
    
    private async Task<bool> IsEmailTakenAsync(string email)
    {
        return await _unitOfWork.UserRepository.IsEmailTakenAsync(email);
    }
    
}