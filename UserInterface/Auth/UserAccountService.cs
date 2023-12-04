using ModelLayer;
using DataAccessLayer;
namespace UserInterface.Auth
{
    public class UserAccountService
    {

        public ModelUserInfo? GetByUserName(string email, string password)
        {
            return DalAuth.Auth(email, password);
        }
    }
}
