using System.Threading.Tasks;
using InfinityComics1.Auth.Models;

namespace InfinityComics1.Auth
{
    public interface IFirebaseAuthService
    {
        Task<FirebaseUser> Login(Credentials credentials);
        Task<FirebaseUser> Register(Registration registration);
    }
}