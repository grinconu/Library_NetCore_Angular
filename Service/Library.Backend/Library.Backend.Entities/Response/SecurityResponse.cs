namespace Library.Backend.Entities.Response
{
    using System;

    public class SecurityResponse
    {
        public string TokenType { get; set; }

        public string AccessToken { get; set; }

        public DateTime Expiration { get; set; }
    }
}