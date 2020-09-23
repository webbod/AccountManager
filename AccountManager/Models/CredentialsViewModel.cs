using AccountManager.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AccountManager.Models
{
    public class CredentialsViewModel : AccountCredentials
    {
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Email address")]
        [StringLength(maximumLength: 200, ErrorMessage = "This email address is too long (max: 200 characters)")]
        [EmailAddress(ErrorMessage = "Please use a valid email address")]
        [Remote(action: "CheckIfEmailIsNotInUse", controller:"Account", ErrorMessage = "This email address is already in use", HttpMethod = "Post")]
        public override string EmailAddress { get => base.EmailAddress; set => base.EmailAddress = value; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Password")]
        [PasswordPropertyText(true)]
        [StringLength(maximumLength: 32, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 32 characters in length")]
        [RegularExpression(pattern: @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^\da-zA-Z]).*$", ErrorMessage = "Password must contain at least one: number, upper-case letter, lower-case letter and a symbol")]
        public override string PlainTextPassword { get => base.PlainTextPassword; set => base.PlainTextPassword = value; }

        public bool WasSaved { get; set; }
    }
}
