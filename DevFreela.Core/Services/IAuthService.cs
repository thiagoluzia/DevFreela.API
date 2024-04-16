namespace DevFreela.Core.Services
{
    public interface IAuthService
    {
        string GenerationJwtToken(string email, string role);
        string ComputeSha256Hash(string password);
    }
}
