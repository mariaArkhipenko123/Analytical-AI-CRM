﻿namespace CRM.CoreService.Application.Models.Inputs
{
    public class CreateUserInput
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Status { get; set; } 
    }
}
