using System.ComponentModel.DataAnnotations;

namespace StockApp.Models
{
    public class LoginModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your Username.")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your Password.")]
        public string Password { get; set; }
    }
}