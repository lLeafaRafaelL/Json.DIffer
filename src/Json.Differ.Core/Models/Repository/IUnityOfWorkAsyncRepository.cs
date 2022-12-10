using Json.Differ.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json.Differ.Core.Models.Repository
{
    public interface IUnityOfWorkAsyncRepository : IRepository
    {
        public IUnityOfWorkAsync UnityOfWork { get; }
    }
}
