using AccountManager.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AccountManager.Models
{
    public class CredentialsViewModel : AccountCredentials
    {
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Email address", Prompt = "Email address")]
        [StringLength(maximumLength: EmailAddressMaxLength)]
        [EmailAddress(ErrorMessage = "Please use a valid email address")]
        [Remote(action: "CheckIfEmailIsNotInUse", controller:"Account", ErrorMessage = "This email address is in use", HttpMethod = "Post")]
        public override string EmailAddress { get => base.EmailAddress; set => base.EmailAddress = value; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Password", Prompt = "Password")]
        [PasswordPropertyText(true)]
        [StringLength(maximumLength: PlainTextPasswordMaxLength, MinimumLength = PlainTextPasswordMinLength)]
        [RegularExpression(pattern: PlainTextPasswordPattern, ErrorMessage = PlainTestPatternErrorMessage)]
        public override string PlainTextPassword { get => base.PlainTextPassword; set => base.PlainTextPassword = value; }

        public bool WasSaved { get; set; }

        public void HasBeenSaved()
        {
            WasSaved= true;
            PlainTextPassword = string.Empty;
        }
    }
}
