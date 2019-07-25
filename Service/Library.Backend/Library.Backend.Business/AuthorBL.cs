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

    public class AuthorBL: ResponseBase<Author>, IAuthorBL
    {
        readonly ILogger<AuthorBL> _logger;

        readonly IGenericRepository<Author> _repoAuthor;

        public AuthorBL(ILogger<AuthorBL> logger, IGenericRepository<Author> repoAuthor)
        {
            _logger = logger;
            _repoAuthor = repoAuthor;
        }

        public Response<Author> Create(AuthorRequest request)
        {
            try
            {
                var messages = request.Validate();

                if (messages != null && messages.Count > 0)
                {
                    return ResponseBadRequest(messages.ToList());
                }

                _repoAuthor.Add(new Author
                {
                    Birthdate = request.Birthdate,
                    LastName = request.Lastname,
                    Name = request.Name
                });

                return ResponseSuccess(null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception Method {nameof(AuthorBL)}.{nameof(Create)}");
                return ResponseFail();
            }
        }

        public Response<Author> Delete(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id) || !int.TryParse(id, out int idAuthor))
                {
                    return ResponseBadRequest(new List<string> { MessagesResponse.BadRequest });
                }

                var author = _repoAuthor.Get(idAuthor);

                if (author == null)
                {
                    return ResponseFail(CodeResponse.NotFoundAuthor, new List<string> { MessagesResponse.NotFoundAuthor });
                }

                _repoAuthor.Delete(author);

                return ResponseSuccess(null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception Method {nameof(AuthorBL)}.{nameof(Delete)}");
                return ResponseFail();
            }
        }

        public Response<Author> Get(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id) || !int.TryParse(id, out int idAuthor))
                {
                    return ResponseBadRequest(new List<string> { MessagesResponse.BadRequest });
                }

                var author = _repoAuthor.Get(idAuthor);

                if (author == null)
                {
                    return ResponseFail(CodeResponse.NotFoundAuthor, new List<string> { MessagesResponse.NotFoundAuthor });
                }

                return ResponseSuccess(new List<Author> { author });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception Method {nameof(AuthorBL)}.{nameof(Get)}");
                return ResponseFail();
            }

        }

        public Response<Author> GetAll()
        {
            try
            {
                return ResponseSuccess(_repoAuthor.GetAll());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception Method {nameof(AuthorBL)}.{nameof(GetAll)}");
                return ResponseFail();
            }
        }

        public Response<Author> Update(string id, AuthorRequest request)
        {
            try
            {
                var messages = request.Validate();
                if (messages != null && messages.Count > 0)
                {
                    return ResponseBadRequest(messages.ToList());
                }

                if (string.IsNullOrEmpty(id) || !int.TryParse(id, out int idAuthor))
                {
                    return ResponseBadRequest(new List<string> { MessagesResponse.BadRequest });
                }

                var author = _repoAuthor.Get(idAuthor);

                if (author == null)
                {
                    return ResponseFail(CodeResponse.NotFoundAuthor, new List<string> { MessagesResponse.NotFoundAuthor });
                }

                _repoAuthor.Update(author, new Author { Birthdate = request.Birthdate, LastName = request.Lastname, Name = request.Name });

                return ResponseSuccess(null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception Method {nameof(AuthorBL)}.{nameof(Update)}");
                return ResponseFail();
            }
        }
    }
}