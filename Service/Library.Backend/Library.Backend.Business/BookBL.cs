namespace Library.Backend.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Library.Backend.Business.Base;
    using Library.Backend.Contracts.Business;
    using Library.Backend.Contracts.Repository;
    using Library.Backend.Entities.Models;
    using Library.Backend.Entities.Refetentials;
    using Library.Backend.Entities.Request;
    using Library.Backend.Utils;
    using Library.Backend.Utils.Enum;
    using Library.Backend.Utils.Messages;
    using Microsoft.Extensions.Logging;

    public class BookBL: ResponseBase<Book>, IBookBL
    {
        readonly ILogger<BookBL> _logger;

        readonly IGenericRepository<Book> _repoBook;

        readonly IGenericRepository<Category> _repoCategory;

        readonly IGenericRepository<Author> _repoAuthor;

        public BookBL(ILogger<BookBL> logger,
            IGenericRepository<Book> repoBook,
            IGenericRepository<Category> repoCategory,
            IGenericRepository<Author> repoAuthor)
        {
            _logger = logger;
            _repoBook = repoBook;
            _repoCategory = repoCategory;
            _repoAuthor = repoAuthor;
        }

        public Response<Book> Create(BookRequest request)
        {
            try
            {
                var messages = request.Validate();

                if (messages != null && messages.Count > 0)
                {
                    return ResponseBadRequest(messages.ToList());
                }

                var category = _repoCategory.Get(request.IdCategory);

                if (category == null)
                {
                    return ResponseFail(CodeResponse.NotFoundCategory, new List<string> { MessagesResponse.NotFoundCategory });
                }

                var author = _repoAuthor.Get(request.IdAuthor);

                if (author == null)
                {
                    return ResponseFail(CodeResponse.NotFoundAuthor, new List<string> { MessagesResponse.NotFoundAuthor });
                }

                _repoBook.Add(new Book {
                    Author = author,
                    Category = category,
                    ISBN = request.ISBN,
                    Name = request.Name
                });

                return ResponseSuccess(null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception Method {nameof(BookBL)}.{nameof(Create)}");
                return ResponseFail();
            }
        }

        public Response<Book> Delete(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id) || !int.TryParse(id, out int idBook))
                {
                    return ResponseBadRequest(new List<string> { MessagesResponse.BadRequest });
                }

                var book = _repoBook.Get(idBook);

                if (book == null)
                {
                    return ResponseFail(CodeResponse.NotFoundBook, new List<string> { MessagesResponse.NotFoundBook });
                }

                _repoBook.Delete(book);

                return ResponseSuccess(null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception Method {nameof(BookBL)}.{nameof(Delete)}");
                return ResponseFail();
            }
        }

        public Response<Book> Get(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id) || !int.TryParse(id, out int idBook))
                {
                    return ResponseBadRequest(new List<string> { MessagesResponse.BadRequest });
                }

                var book = _repoBook.Get(idBook);

                if (book == null)
                {
                    return ResponseFail(CodeResponse.NotFoundBook, new List<string> { MessagesResponse.NotFoundBook });
                }

                book.Category = _repoCategory.Get(book.IdCategory);
                book.Author = _repoAuthor.Get(book.IdAuthor);

                return ResponseSuccess(new List<Book> { book });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception Method {nameof(BookBL)}.{nameof(Get)}");
                return ResponseFail();
            }
        }

        public Response<Book> GetAll()
        {
            try
            {
                var books =_repoBook.GetAll().ToList();

                books.ForEach(book=> {
                    book.Category = _repoCategory.Get(book.IdCategory);
                    book.Author = _repoAuthor.Get(book.IdAuthor);
                });

                return ResponseSuccess(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception Method {nameof(BookBL)}.{nameof(Get)}");
                return ResponseFail();
            }
        }

        public Response<Book> Update(string id, BookRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(id) || !int.TryParse(id, out int idBook))
                {
                    return ResponseBadRequest(new List<string> { MessagesResponse.BadRequest });
                }

                var messages = request.Validate();

                if (messages != null && messages.Count > 0)
                {
                    return ResponseBadRequest(messages.ToList());
                }

                var book = _repoBook.Get(idBook);

                if (book == null)
                {
                    return ResponseFail(CodeResponse.NotFoundBook, new List<string> { MessagesResponse.NotFoundBook });
                }

                var category = _repoCategory.Get(request.IdCategory);

                if (category == null)
                {
                    return ResponseFail(CodeResponse.NotFoundCategory, new List<string> { MessagesResponse.NotFoundCategory });
                }

                var author = _repoAuthor.Get(request.IdAuthor);

                if (author == null)
                {
                    return ResponseFail(CodeResponse.NotFoundAuthor, new List<string> { MessagesResponse.NotFoundAuthor });
                }

                _repoBook.Update(book, new Book
                {
                    Author = author,
                    Category = category,
                    ISBN = request.ISBN,
                    Name = request.Name
                });

                return ResponseSuccess(null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception Method {nameof(BookBL)}.{nameof(Create)}");
                return ResponseFail();
            }
        }
    }
}
