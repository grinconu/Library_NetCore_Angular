namespace Library.Backend.Contracts.Repository
{
    using System.Collections.Generic;

    public interface IGenericRepository<T> where T : class, new()
    {
        IList<T> GetAll();

        T Get(int id);

        void Add(T entity);

        void Update(T dbEntity, T entity);

        void Delete(T entity);
    }
}