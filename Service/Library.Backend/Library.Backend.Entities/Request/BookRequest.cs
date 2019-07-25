namespace Library.Backend.Entities.Request
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class BookRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nombre del libro requerido.")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Author del libro requerido.")]
        public int IdAuthor { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Categoria del libro requerida.")]
        public int IdCategory { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "ISBN del libro requerida.")]
        public string ISBN { get; set; }
    }
}
