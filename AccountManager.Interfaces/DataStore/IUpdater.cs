namespace AccountManager.Interfaces.DataStore
{
    public interface IUpdater<TConfiguration, TOutput> : IInitialisable
    {
        TOutput Update(TConfiguration configuration);
    }
}