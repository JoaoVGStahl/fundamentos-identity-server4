using SkyCommerce.Extensions;
using System.ComponentModel.DataAnnotations;

namespace SkyCommerce.Site.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Preencha o campo E-mail")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Informe uma senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Lembrar?")]
        public bool RememberMe { get; set; }


        public bool IsUsernameEmail()
        {
            // Return true if strIn is in valid e-mail format.
            return Username.IsEmail();
        }
    }
}
