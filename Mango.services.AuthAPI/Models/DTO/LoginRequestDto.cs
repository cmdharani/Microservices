﻿namespace Mango.services.AuthAPI.Models.DTO
{
    public class LoginRequestDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
