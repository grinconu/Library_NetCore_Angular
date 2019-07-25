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

    public class CategoryBL : ResponseBase<Category>, ICategoryBL
    {
        readonly ILogger<CategoryBL> _logger;

        readonly IGenericRepository<Category> _repoCategory;

        public CategoryBL(ILogger<CategoryBL> logger, IGenericRepository<Category> repoCategory)
        {
            _logger = logger;
            _repoCategory = repoCategory;
        }

        public Response<Category> Create(CategoryRequest request)
        {
            try
            {
                var messages = request.Validate();

                if (messages != null && messages.Count > 0)
                {
                    return ResponseBadRequest(messages.ToList());
                }

                _repoCategory.Add(new Category
                {
                    Description = request.Description,
                    Name = request.Name
                });

                return ResponseSuccess(null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception Method {nameof(CategoryBL)}.{nameof(Create)}");
                return ResponseFail();
            }
        }

        public Response<Category> Delete(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id) || !int.TryParse(id, out int idCategory))
                {
                    return ResponseBadRequest(new List<string> { MessagesResponse.BadRequest });
                }

                var category = _repoCategory.Get(idCategory);

                if (category == null)
                {
                    return ResponseFail(CodeResponse.NotFoundCategory, new List<string> { MessagesResponse.NotFoundCategory });
                }

                _repoCategory.Delete(category);

                return ResponseSuccess(null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception Method {nameof(CategoryBL)}.{nameof(Delete)}");
                return ResponseFail();
            }
        }

        public Response<Category> Get(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id) || !int.TryParse(id, out int idCategory))
                {
                    return ResponseBadRequest(new List<string> { MessagesResponse.BadRequest });
                }

                var category = _repoCategory.Get(idCategory);

                if (category == null)
                {
                    return ResponseFail(CodeResponse.NotFoundCategory, new List<string> { MessagesResponse.NotFoundCategory });
                }

                return ResponseSuccess(new List<Category> { category });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception Method {nameof(CategoryBL)}.{nameof(Get)}");
                return ResponseFail();
            }
        }

        public Response<Category> GetAll()
        {
            try
            {
                return ResponseSuccess(_repoCategory.GetAll());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception Method {nameof(CategoryBL)}.{nameof(GetAll)}");
                return ResponseFail();
            }
        }

        public Response<Category> Update(string id, CategoryRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(id) || !int.TryParse(id, out int idCategory))
                {
                    return ResponseBadRequest(new List<string> { MessagesResponse.BadRequest });
                }

                var messages = request.Validate();

                if (messages != null && messages.Count > 0)
                {
                    return ResponseBadRequest(messages.ToList());
                }

                var category = _repoCategory.Get(idCategory);

                if (category == null)
                {
                    return ResponseFail(CodeResponse.NotFoundCategory, new List<string> { MessagesResponse.NotFoundCategory });
                }

                _repoCategory.Update(category, new Category { Description = request.Description, Name= request.Name});

                return ResponseSuccess(null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception Method {nameof(CategoryBL)}.{nameof(Update)}");
                return ResponseFail();
            }
        }
    }
}
