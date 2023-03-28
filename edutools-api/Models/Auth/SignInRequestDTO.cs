namespace edutools_api.Models.Auth
{
    public class SignInRequestDTO
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
