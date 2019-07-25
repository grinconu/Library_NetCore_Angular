namespace Library.Backend.Contracts.Business
{
    using Library.Backend.Entities.Refetentials;
    using Library.Backend.Entities.Request;
    using Library.Backend.Entities.Response;

    public interface ISecurityBL
    {
        Response<SecurityResponse> ValidateUser(SecurityRequest request);

        Response<SecurityResponse> RefreshToken(string token    );
    }
}
