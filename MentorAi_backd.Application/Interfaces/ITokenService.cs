using MentorAi_backd.Domain.Entities.UserEntity;

namespace MentorAi_backd.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
    }
}
