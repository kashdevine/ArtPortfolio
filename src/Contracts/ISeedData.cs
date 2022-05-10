namespace ArtPortfolio.Contracts
{
    public interface ISeedData
    {
        /// <summary>
        /// Creates the base admin user for the app.
        /// </summary>
        public Task<bool> SeedAdminUser();
    }
}
