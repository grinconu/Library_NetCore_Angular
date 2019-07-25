namespace Library.Backend.Entities.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAuthor { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public DateTime Birthdate { get; set; }
    }
}
