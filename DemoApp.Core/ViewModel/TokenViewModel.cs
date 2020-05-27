using DemoApp.Core.Attribute;
using System;
using System.ComponentModel.DataAnnotations;

namespace DemoApp.Core.ViewModel
{
    public partial class TokenViewModel
    {
        public string Roles { get; set; }
        public string Access_token { get; set; }
        public DateTime Expires_in { get; set; }
        public string Avartar { get; set; }
        public string FullName { get; set; }
    }

    public partial class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [CheckName(Property = "Fullname")]
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Avartar { get; set; }
        public int? Phone { get; set; }
    }
    public partial class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
