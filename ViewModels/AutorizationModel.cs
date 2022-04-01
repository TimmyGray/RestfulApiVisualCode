using System.ComponentModel.DataAnnotations;

namespace RestfulApiVisualCode.ViewModels
{
    public class AutorizationModel
    {
        [Required(ErrorMessage ="Неправильный логин!")]
        public string Login { get; set; }

        [Required(ErrorMessage ="Не указан пароль")]
        [DataType(DataType.Password)]
        public  string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Неверный пароль")]
        public string ConfirmPassword { get; set; }

    }
}
