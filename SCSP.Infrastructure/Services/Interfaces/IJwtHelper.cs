using SCSP.Domain.Commons.Response;
namespace SCSP.Infrastructure.Services.Interfaces;

public interface IJwtHelper
{
    public JwtResponse CreateJwtAsync(int userId);
    public Task<int> DecodJwt(string accessToken);
}
