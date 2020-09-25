namespace AccountManager.Interfaces.DataStore
{
    public interface IInserter<TConfiguration, TOutput> : IInitialisable
    { 
        TOutput Insert(TConfiguration configuration);
    }
}