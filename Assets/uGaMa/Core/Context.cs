using uGaMa.Command;
using uGaMa.Observer;
using UnityEngine;

namespace uGaMa.Core
{
    [ScriptOrder(-10000)]
    public class Context : MonoBehaviour, IContext
    {
        GameManager _gameManager;
        DispatchManager _dispatcher;
        CommandBinder _commandMap;

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

        public CommandBinder CommandMap
        {
            get
            {
                return _commandMap;
            }
        }

        public void Awake()
        {
            Debug.Log("Controller Awake");
            _gameManager = GameManager.Instance;
            _dispatcher = _gameManager.Dispatcher;
            _commandMap = _gameManager.CommandMap;
            _gameManager.AddContext(this);
            OnRegister();
        }

        public void OnDestroy()
        {
            _gameManager.RemoveContext(this);
            OnRemove();
        }

        virtual public void OnRemove() { }

        virtual public void OnRegister() {}


    }
}
