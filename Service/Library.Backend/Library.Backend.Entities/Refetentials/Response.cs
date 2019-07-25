namespace Library.Backend.Entities.Refetentials
{
    using System.Collections.Generic;

    public class Response<T>
    {

        public bool TransactionComplete { get; set; }

        public int ResponseCode { get; set; }

        public IList<string> Message { get; set; }

        public IList<T> Data { get; set; }
    }
}