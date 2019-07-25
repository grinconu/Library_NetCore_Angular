namespace Library.Backend.Contracts.Business
{
    using Library.Backend.Entities.Models;
    using Library.Backend.Entities.Refetentials;
    using Library.Backend.Entities.Request;

    public interface IBookBL
    {
        Response<Book> GetAll();

        Response<Book> Get(string id);

        Response<Book> Create(BookRequest request);

        Response<Book> Update(string id, BookRequest request);

        Response<Book> Delete(string id);
    }
}
