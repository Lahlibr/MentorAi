using MentorAi_backd.Repositories.Interfaces;

namespace MentorAi_backd.Repositories.Implementations
{
    public class AuthRepo : IAuthRepo
    {
        private readonly MentorAi_backd _Backd;
        private readonly ITokenRepo _TokenRepo;
    }
}
