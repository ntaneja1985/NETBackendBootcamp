

namespace Shared.Contracts.CQRS
{
    public interface IQuery<out T> : IRequest<T>
    {

    }
}
