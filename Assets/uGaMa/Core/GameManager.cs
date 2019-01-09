using System;
using System.Collections.Generic;
using uGaMa.Command;
using uGaMa.Observer;
using UnityEngine;

namespace uGaMa.Core
{
    [ScriptOrder(-10002)]
    public partial class GameManager : Singleton<GameManager>
    {
        private CommandBinder _commandManager;

        private Dictionary<Type, IContext> _contexts;

        public DispatchManager Dispatcher
        {
            get { return DispatchManager.Instance; }
        }

        public CommandBinder CommandMap
        {
            get
            {
                if (Instance != null && Instance._commandManager == null)
                    Instance._commandManager = new CommandBinder();
                return Instance._commandManager;
            }
        }

        public void Awake()
        {
            _contexts = new Dictionary<Type, IContext>();
        }

        internal T GetContext<T>()
        {
            return (T) _contexts[typeof(T)];
        }

        internal void AddContext(IContext controller)
        {
            _contexts.Add(controller.GetType(), controller);
        }

        internal void RemoveContext(IContext controller)
        {
            _contexts.Remove(controller.GetType());
        }
    }
}