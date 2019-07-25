namespace Library.Backend.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Library.Backend.Contracts.Repository;
    using Library.Backend.Entities.Models;

    public class AuthorDB: IGenericRepository<Author>
    {
        readonly LibraryContext _libraryContext;

        public AuthorDB(LibraryContext context)
        {
            _libraryContext = context;
        }

        public void Add(Author entity)
        {
            _libraryContext.Authors.Add(entity);
            _libraryContext.SaveChanges();
        }

        public void Delete(Author entity)
        {
            _libraryContext.Authors.Remove(entity);
            _libraryContext.SaveChanges();
        }

        public Author Get(int id)
        {
            return _libraryContext.Authors.FirstOrDefault(author => author.IdAuthor == id);
        }

        public IList<Author> GetAll()
        {
            return _libraryContext.Authors.ToList();
        }

        public void Update(Author dbEntity, Author entity)
        {
            dbEntity.LastName = entity.LastName;
            dbEntity.Name = entity.Name;
            dbEntity.Birthdate = entity.Birthdate;

            _libraryContext.SaveChanges();
        }
    }
}
