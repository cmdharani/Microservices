﻿namespace Mango.services.AuthAPI.Models.DTO
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
