namespace Library.Backend.Contracts.Business
{
    using Library.Backend.Entities.Models;
    using Library.Backend.Entities.Refetentials;
    using Library.Backend.Entities.Request;

    public interface IAuthorBL
    {
        Response<Author> GetAll();

        Response<Author> Get(string id);

        Response<Author> Create(AuthorRequest request);

        Response<Author> Update(string id, AuthorRequest request);

        Response<Author> Delete(string id);
    }
}