using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.CoreService.Application.Models.DTOs
{
    public class SmtpSettingsDTO
    {
        public string Provider { get; set; }  
        public int Port { get; set; }   
        public string From { get; set; } 
        public string Password { get; set; } 
    }
}
