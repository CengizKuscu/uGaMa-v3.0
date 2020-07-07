using System;
using System.Collections.Generic;
using uGaMa.Command;
using uGaMa.Observer;
using UnityEngine;

namespace uGaMa.Core
{
    [ScriptOrder(-10001)]
    public partial class GameManager : Singleton<GameManager>
    {
        private CommandBinder _commandManager;
        

        private Dictionary<Type, IContext> _contexts;

        public void Awake()
        {
            _contexts = new Dictionary<Type, IContext>();
        }

        internal T GetContext<T>()
        {
            return (T)_contexts[typeof(T)];
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
