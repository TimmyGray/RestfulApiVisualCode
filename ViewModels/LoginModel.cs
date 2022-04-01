using System.ComponentModel.DataAnnotations;


namespace RestfulApiVisualCode.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Неверный логин")]
        public string Login { get; set; }

        [Required(ErrorMessage ="Неверный пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
