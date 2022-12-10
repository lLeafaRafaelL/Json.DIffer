using System.Data;

namespace Json.Differ.Core.Data
{
    public interface IUnityOfWorkAsync : IUnityOfWorkBase
    {
        IUnityOfWorkAsyncTransaction CurrentTransaction { get; }

        Task<IUnityOfWorkAsyncTransaction> BeginTransactionAsync(CancellationToken cancellationToken);

        Task<IUnityOfWorkAsyncTransaction> BeginTransactionAsync(
            IsolationLevel isolation, CancellationToken cancellationToken);
    }
}
