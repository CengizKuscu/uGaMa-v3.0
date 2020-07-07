using uGaMa.Core;
using uGaMa.Observer;

namespace uGaMa.Command
{
    public class Command : ICommand
    {
        GameManager _gameManager;
        DispatchManager _dispatcher;

        public Command()
        {
            _gameManager = GameManager.Instance;
        }

        internal GameManager gameManager
        {
            get
            {
                return _gameManager;
            }
        }

        public DispatchManager Dispatcher
        {
            get
            {
                return DispatchManager.Instance;
            }
        }

        virtual public void Execute(ObserverParam notify) { }
    }
}
