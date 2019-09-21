using System.Threading.Tasks;

namespace Core.Domain.Events
{
    public interface IHandler<T> where T : Message
    {
        Task Handle(T message);
    }
}
