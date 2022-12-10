using Json.Differ.Core.Models.Repository;
using Microsoft.EntityFrameworkCore;

namespace Json.Differ.Core.Data.EntityFrameworkCore
{
    public class EFUnityOfWorkAsyncRepository : IUnityOfWorkAsyncRepository
    {
        public EFUnityOfWorkAsyncRepository(EFUnityOfWorkAsync efUnityOfWork)
        {
            UnityOfWork = efUnityOfWork;
            EfUnityOfWorkImpl = efUnityOfWork.UnityOfWorkImpl;
        }
        protected DbContext EfUnityOfWorkImpl { get; private set; }
        public IUnityOfWorkAsync UnityOfWork { get; private set; }
    }
}
