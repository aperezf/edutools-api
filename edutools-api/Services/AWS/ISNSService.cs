namespace edutools_api.Services.AWS
{
    public interface ISNSService
    {
        Task<bool> EmitSNSEvent(string type, object detail);
    }
}
