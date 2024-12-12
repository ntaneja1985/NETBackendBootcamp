
namespace Shared.Contracts.CQRS
{
    //Here Unit represents a void type since void is not a valid return type
    public interface ICommand : ICommand<Unit>
    {

    }
    public interface ICommand<out TResponse>: IRequest<TResponse>
    {

    }
}
