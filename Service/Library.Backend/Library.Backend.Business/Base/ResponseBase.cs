namespace Library.Backend.Business.Base
{
    using System.Collections.Generic;
    using Library.Backend.Entities.Refetentials;
    using Library.Backend.Utils.Enum;
    using Library.Backend.Utils.Messages;

    public class ResponseBase<T>
    {
        Response<T> ResponseBusiness;

        public ResponseBase()
        {
            ResponseBusiness = new Response<T>
            {
                ResponseCode = 0,
                Message = new List<string>(),
                TransactionComplete = false,
                Data = new List<T>()
            };
        }

        public Response<T> ResponseSuccess(IList<T> data)
        {
            ResponseBusiness.TransactionComplete = true;
            ResponseBusiness.ResponseCode = (int)CodeResponse.OK;
            ResponseBusiness.Message.Add(MessagesResponse.Ok);
            ResponseBusiness.Data = data;

            return ResponseBusiness;
        }

        public Response<T> ResponseFail()
        {
            ResponseBusiness.TransactionComplete = false;
            ResponseBusiness.ResponseCode = (int)CodeResponse.InternalError;
            ResponseBusiness.Message.Add(MessagesResponse.InternalError);
            return ResponseBusiness;
        }

        public Response<T> ResponseFail(CodeResponse code, IList<string> messages)
        {
            ResponseBusiness.TransactionComplete = false;
            ResponseBusiness.ResponseCode = (int)code;
            ResponseBusiness.Message = messages;
            return ResponseBusiness;
        }

        public Response<T> ResponseBadRequest(IList<string> messages)
        {
            ResponseBusiness.TransactionComplete = false;
            ResponseBusiness.ResponseCode = (int)CodeResponse.BadRequest;
            ResponseBusiness.Message = messages;
            return ResponseBusiness;
        }
    }
}
