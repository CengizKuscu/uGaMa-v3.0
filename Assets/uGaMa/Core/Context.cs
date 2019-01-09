using uGaMa.Command;
using uGaMa.Observer;
using UnityEngine;

namespace uGaMa.Core
{
    [ScriptOrder(-10000)]
    public class Context : MonoBehaviour, IContext
    {
        GameManager _gameManager;
        //DispatchManager _dispatcher;
        CommandBinder _commandMap;

        internal GameManager gameManager
        {
            get
            {
                return GameManager.Instance;
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
                return GameManager.Instance.CommandMap;
            }
        }

        public void Awake()
        {
            _gameManager = GameManager.Instance;
            //_dispatcher = DispatchManager.Instance;
            _commandMap = CommandMap;
            gameManager.AddContext(this);
            OnRegister();
        }

        public void OnDestroy()
        {
            if (GameManager.ApplicationIsQuitting)
                return;
            gameManager.RemoveContext(this);
            OnRemove();
        }

        public virtual void OnRegister() {}

        public virtual void OnRemove() {}
    }
}
