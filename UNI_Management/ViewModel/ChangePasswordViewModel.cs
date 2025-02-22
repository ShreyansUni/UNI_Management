using System.ComponentModel.DataAnnotations;

namespace UNI_Management.ViewModel
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Please Enter Password")]
        public string OldPassword {  get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        public string ConfirmPassword { get; set; }
        public string Email {  get; set; }
    }
}
