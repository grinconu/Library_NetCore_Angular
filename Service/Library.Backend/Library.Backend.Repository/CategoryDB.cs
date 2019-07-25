namespace Library.Backend.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Library.Backend.Contracts.Repository;
    using Library.Backend.Entities.Models;

    public class CategoryDB : IGenericRepository<Category>
    {
        readonly LibraryContext _libraryContext;

        public CategoryDB(LibraryContext context)
        {
            _libraryContext = context;
        }

        public void Add(Category entity)
        {
            _libraryContext.Categories.Add(entity);
            _libraryContext.SaveChanges();
        }

        public void Delete(Category entity)
        {
            _libraryContext.Categories.Remove(entity);
            _libraryContext.SaveChanges();
        }

        public Category Get(int id)
        {
            return _libraryContext.Categories.FirstOrDefault(category => category.IdCategory == id);
        }

        public IList<Category> GetAll()
        {
            return _libraryContext.Categories.ToList();
        }

        public void Update(Category dbEntity, Category entity)
        {
            dbEntity.Name = entity.Name;
            dbEntity.Description = entity.Description;

            _libraryContext.SaveChanges();
        }
    }
}