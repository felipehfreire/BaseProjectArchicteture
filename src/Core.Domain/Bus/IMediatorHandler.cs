using Core.Domain.Commands;
using Core.Domain.Events;
using System.Threading.Tasks;

namespace Core.Domain.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T Command) where T : Command;

        Task RaiseEvent<T>(T Event) where T : Event;
    }
}
