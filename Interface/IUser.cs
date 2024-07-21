using TaskManager.Models;

namespace TaskManager.Interface
{
    public interface IUser
    {
        public IEnumerable<User> AllUsers();
        public User? GetEmailandPassword(string email, string password);
        public UserDto Authenticate(User user);
        public User RegisterUser(User user);

        public User UnregisterUser(int id);

    }
}
