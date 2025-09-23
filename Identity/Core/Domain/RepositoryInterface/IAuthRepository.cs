namespace Core.Domain.RepositoryInterface;

public interface IAuthRepository
{
    /// <summary>
    /// Registers a new user in the system.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<User> RegisterAsync(User user);

    /// <summary>
    /// Retrieves a user by their username.
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
	Task<User?> GetByUsernameAsync(string username);
}
