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
            _dispatcher = _gameManager.Dispatcher;
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
                return _dispatcher;
            }
        }

        virtual public void Execute(ObserverParam notify) { }
    }
}
