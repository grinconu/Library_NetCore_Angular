namespace Library.Backend.Entities.Request
{
    using System.ComponentModel.DataAnnotations;

    public class SecurityRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Usuario requerido.")]
        public string User { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password requerido.")]
        public string Password { get; set; }
    }
}