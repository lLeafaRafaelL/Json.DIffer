namespace Json.Differ.Core.Data
{
    public interface IUnityOfWorkAsyncTransaction : IAsyncDisposable
    {
        bool Completed { get; }
        Guid? TransactionId { get; }
        Task CommitAsync(CancellationToken token);
        Task RollbackAsync(CancellationToken token);
    }
}
