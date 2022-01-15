﻿using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UniversityProject.Data.Entities;
using UniversityProject.Data.Repositories.Interfaces;
using UniversityProject.Domain.Dto.User;
using UniversityProject.Domain.Exceptions;
using UniversityProject.Domain.Services.Interfaces;

namespace UniversityProject.Domain.Services.Implementation;

public class UserService: IUserService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<User> _passwordHasher;
    
    public UserService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
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

    private async Task<bool> IsEmailTakenAsync(string email)
    {
        return await _unitOfWork.UserRepository.IsEmailTakenAsync(email);
    }
    
}