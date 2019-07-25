namespace Library.Backend.Entities.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdBook { get; set; }

        public string Name { get; set; }

        public int IdAuthor { get; set; }

        public Author Author { get; set; }

        public int IdCategory { get; set; }

        public Category Category { get; set; }

        public string ISBN { get; set; }
    }
}
