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

namespace UniversityProject.Domain.Services.Implementation;

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
            throw new ResultException("Account already exists", HttpStatusCode.Conflict);
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
            
            throw new ResultException( "Cannot create account.",HttpStatusCode.InternalServerError);
        }
    }

    public async Task<UpdateUserDto> GetCurrentUserAsync()
    {
        var id = _context.HttpContext.User.GetCurrentUserId();
        var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
        return _mapper.Map<UpdateUserDto>(user);
    }
    
    
    public async Task UpdateUserAsync(UpdateUserDto model)
    {
        var user = _mapper.Map<User>(model);
        user.Id = _context.HttpContext.User.GetCurrentUserId();
        await _unitOfWork.UserRepository.UpdateAsync(user);
        await _unitOfWork.SaveAsync();
    }
    
    private async Task<bool> IsEmailTakenAsync(string email)
    {
        return await _unitOfWork.UserRepository.IsEmailTakenAsync(email);
    }
    
}