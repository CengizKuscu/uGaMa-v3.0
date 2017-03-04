using uGaMa.Observer;

namespace uGaMa.Command
{
    public interface ICommand
    {
        void Execute(ObserverParam notify);
    }
}
