﻿namespace UniversityProject.Domain.Models;

public enum ErrorCode
{
    Ok = 200,
    ValidationError = 400,
    Unauthorized = 401,
    Forbidden = 403,
    NotFound = 404,
    Conflict = 409,
    UnprocessableEntity = 422,
    InternalServerError = 500,
}