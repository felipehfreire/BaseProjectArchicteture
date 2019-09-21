using System;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnityOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}
