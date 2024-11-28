using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public String FirstName { get; set; } = string.Empty;
        [Required]
        public String LastName { get; set; } = string.Empty;
        [Required]
        public String Email { get; set; } = string.Empty;
        [Required]
        public String Password { get; set; } = string.Empty;
    }
}