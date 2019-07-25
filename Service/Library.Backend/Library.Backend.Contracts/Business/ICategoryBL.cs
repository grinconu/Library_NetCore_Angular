namespace Library.Backend.Contracts.Business
{
    using System;
    using Library.Backend.Entities.Models;
    using Library.Backend.Entities.Refetentials;
    using Library.Backend.Entities.Request;

    public interface ICategoryBL
    {
        Response<Category> GetAll();

        Response<Category> Get(string id);

        Response<Category> Create(CategoryRequest request);

        Response<Category> Update(string id, CategoryRequest request);

        Response<Category> Delete(string id);
    }
}
