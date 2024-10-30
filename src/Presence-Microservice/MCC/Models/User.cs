using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace MCC.Models
{
    public class User : IdentityUser
    {
        public override string Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public override string PasswordHash { get; set; }

        [Required]
        [EmailAddress]
        public override string Email { get; set; }

        [Required]
        public string Period { get; set; }

        [Required]
        public string Role { get; set; }

        public User() : base() { }


    }
}
