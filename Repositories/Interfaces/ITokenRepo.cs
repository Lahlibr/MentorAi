using MentorAi_backd.Models.Entity;

namespace MentorAi_backd.Repositories.Interfaces
{
    public interface ITokenRepo
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
    }
}
