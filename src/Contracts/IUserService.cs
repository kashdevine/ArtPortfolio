namespace ArtPortfolio.Contracts
{
    public interface IUserService
    {
        /// <summary>
        /// Logins the user
        /// </summary>
        /// <param name="username">String</param>
        /// <param name="password">String</param>
        /// <returns>A bool</returns>
        public Task<bool> Login(string username, string password);
        /// <summary>
        /// Logs the user out
        /// </summary>
        /// <param name="username">String</param>
        /// <param name="password">String</param>
        /// <returns>A bool</returns>
        public Task<bool> Logout(string username, string password);
        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="username">String</param>
        /// <param name="password">String</param>
        /// <returns>A bool</returns>
        public Task<bool> Register(string username, string password);
        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>A bool</returns>
        public Task<bool> DeleteUser(Guid id);
        /// <summary>
        /// Updates a user
        /// </summary>
        /// <param name="id">Guid</param>
        /// <param name="username">String</param>
        /// <param name="password">String</param>
        /// <returns>A bool</returns>
        public Task<bool> UpdateUser(Guid id, string username, string password);
    }
}
