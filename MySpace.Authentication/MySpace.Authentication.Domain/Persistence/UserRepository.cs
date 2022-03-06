using MySpace.Authentication.Domain.Models;

namespace MySpace.Authentication.Domain.Persistence;

public interface UserRepository
{
    User AddUser(User user);
    User GetUser(string id);
    User EditUser(string id, User user);
    void DeleteUser(string id);
}