namespace Library.Backend.Entities.Request
{
    using System.ComponentModel.DataAnnotations;

    public class CategoryRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nombre de la categoria requerida.")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Descripcion de la categoria requerida.")]
        public string Description { get; set; }
    }
}
