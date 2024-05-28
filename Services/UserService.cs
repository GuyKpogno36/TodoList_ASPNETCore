using Todo_List_ASPNETCore.Models;

namespace Todo_List_ASPNETCore.Services
{
    public interface IUserService
    {
        Task RegisterAsync(LoginRequest model);
        Task<UserProfileModel> GetProfile(string userId);
    }

    public class UserService : IUserService
    {
        public async Task RegisterAsync(LoginRequest model)
        {
            // Implémentation de l'enregistrement de l'utilisateur
        }

        public async Task<UserProfileModel> GetProfile(string userId)
        {
            // Implémentation pour récupérer le profil de l'utilisateur
            return null;
        }
    }
}
