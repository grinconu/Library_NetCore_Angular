namespace Library.Backend.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Library.Backend.Business.Base;
    using Library.Backend.Contracts.Business;
    using Library.Backend.Entities.Refetentials;
    using Library.Backend.Entities.Request;
    using Library.Backend.Entities.Response;
    using Library.Backend.Utils;
    using Library.Backend.Utils.Messages;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class SecurityBL : ResponseBase<SecurityResponse>, ISecurityBL
    {
        readonly InfoJWT _infoJWT;

        static List<SecurityResponse> TokenJWT = new List<SecurityResponse>();

        readonly ILogger<SecurityBL> _logger;

        public SecurityBL(IOptions<InfoJWT> infoJWT, ILogger<SecurityBL> logger)
        {
            _infoJWT = infoJWT.Value;
            _logger = logger;
        }

        public Response<SecurityResponse> RefreshToken(string token)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    return ResponseBadRequest(new List<string> { MessagesResponse.BadRequest });
                }

                var itemJWT = TokenJWT.Where(item => item.AccessToken == token).FirstOrDefault();

                if (itemJWT == null)
                {
                    return ResponseBadRequest(new List<string> { MessagesResponse.BadRequest });
                }

                var time = int.Parse(_infoJWT.TimeMin);

                var response = new SecurityResponse
                {
                    AccessToken = ManagerToken.GenerateToken(_infoJWT.Key, time, _infoJWT.User),
                    Expiration = DateTime.Now.AddMinutes(time),
                    TokenType = "Bearer"
                };

                TokenJWT.Remove(itemJWT);
                TokenJWT.Add(response);

                return ResponseSuccess(new List<SecurityResponse> { response });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception Method {nameof(RefreshToken)}");
                return ResponseFail();
            }
        }

        public Response<SecurityResponse> ValidateUser(SecurityRequest request)
        {
            try
            {
                var messages = request.Validate();

                if (messages != null && messages.Count > 0)
                {
                    return ResponseBadRequest(messages.ToList());
                }

                if (request.User != _infoJWT.User ||
                   request.Password != _infoJWT.Password)
                {
                    return ResponseBadRequest(new List<string> { MessagesResponse.BadRequest });
                }

                var time = int.Parse(_infoJWT.TimeMin);

                var response = new SecurityResponse
                {
                    AccessToken = ManagerToken.GenerateToken(_infoJWT.Key, time, request.User),
                    Expiration = DateTime.Now.AddMinutes(time),
                    TokenType = "Bearer"
                };

                TokenJWT.Add(response);

                return ResponseSuccess(new List<SecurityResponse> { response });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception Method {nameof(ValidateUser)}");

                return ResponseFail();
            }
        }
    }
}