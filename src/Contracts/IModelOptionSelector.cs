namespace ArtPortfolio.Contracts
{
    public interface IModelOptionSelector<T> where T : class
    {
        public Task SelectAsync(Guid id);
    }
}
