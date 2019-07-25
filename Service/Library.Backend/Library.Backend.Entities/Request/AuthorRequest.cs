namespace Library.Backend.Entities.Request
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AuthorRequest
    {
        [Required(AllowEmptyStrings =false, ErrorMessage = "Nombre del autor requerido.")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Apellido del autor requerido.")]
        public string Lastname { get; set; }

        public DateTime Birthdate { get; set; }
    }
}