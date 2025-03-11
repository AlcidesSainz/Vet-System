using System.ComponentModel.DataAnnotations;

namespace Vet_Application.DTOs.UsersEntity
{
    public class UserCredentialsDTO
    {
        [EmailAddress]
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
