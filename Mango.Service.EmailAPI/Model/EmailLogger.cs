﻿namespace Mango.Service.EmailAPI.Model
{
    public class EmailLogger
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Message { get; set; }=string.Empty;
        public DateTime? EmailSent { get; set; }
    }
}
