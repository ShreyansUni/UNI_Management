using System.ComponentModel.DataAnnotations;

namespace UNI_Management.ViewModel
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Please Enter your EmailAddress")]
        public string email {  get; set; }
    }
}
