using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.CoreService.Application.Models.DTOs
{
    public class TokensResponseDTO
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
