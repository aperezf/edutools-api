namespace edutools_api.Services.AWS
{
    public interface IEmailService
    {
        Task<bool> SendEmail(string to, string body);
    }
}
