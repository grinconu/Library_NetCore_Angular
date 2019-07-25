namespace Library.Backend.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Library.Backend.Contracts.Repository;
    using Library.Backend.Entities.Models;

    public class BookDB: IGenericRepository<Book>
    {
        readonly LibraryContext _libraryContext;

        public BookDB(LibraryContext context)
        {
            _libraryContext = context;
        }

        public void Add(Book entity)
        {
            _libraryContext.Books.Add(entity);
            _libraryContext.SaveChanges();
        }

        public void Delete(Book entity)
        {
            _libraryContext.Books.Remove(entity);
            _libraryContext.SaveChanges();
        }

        public Book Get(int id)
        {
            return _libraryContext.Books.FirstOrDefault(book=> book.IdBook == id);
        }

        public IList<Book> GetAll()
        {
            return _libraryContext.Books.ToList();
        }

        public void Update(Book dbEntity, Book entity)
        {
            dbEntity.Author = entity.Author;
            dbEntity.Category = entity.Category;
            dbEntity.ISBN = entity.ISBN;
            dbEntity.Name = entity.Name;

            _libraryContext.SaveChanges();
        }
    }
}