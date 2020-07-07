using uGaMa.Command;
using uGaMa.Observer;
using UnityEngine;

namespace uGaMa.Core
{
    [ScriptOrder(-10000)]
    public class Context : MonoBehaviour, IContext
    {
        GameManager _gameManager;
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
                return DispatchManager.Instance;
            }
        }

        public CommandBinder CommandMap
        {
            get
            {
                return Dispatcher.CommandMap;
            }
        }

        public void Awake()
        {
            _gameManager = GameManager.Instance;
            _gameManager.AddContext(this);
            OnRegister();
        }

        public void OnDestroy()
        {
            _gameManager.RemoveContext(this);
            OnRemove();
        }

        public virtual void OnRegister() {}

        public virtual void OnRemove() {}
    }
}
