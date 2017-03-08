using System;
using System.Collections.Generic;
using uGaMa.Core;
using uGaMa.Observer;
using UnityEngine;

namespace uGaMa.Views
{
    public class View : MonoBehaviour, IObserver
    {
        GameManager _gameManager;

        DispatchManager _dispatcher;

        List<object> dispatchKeys = new List<object>();

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

        public void Awake()
        {
            _gameManager = GameManager.Instance;
            _dispatcher = _gameManager.Dispatcher;
            OnRegister();
        }

        public void OnDestroy()
        {
            if (dispatchKeys.Count > 0)
            {
                Dispatcher.RemoveAllListeners(this);
            }
            OnRemove();
        }

        public virtual void OnRegister() { }
        public virtual void OnRemove() { }

        public List<object> DispatchKeys
        {
            get
            {
                return dispatchKeys;
            }
        }

        public void OnHandlerObserver(ObserverParam param, Action<ObserverParam> callBack)
        {
            callBack(param);
        }
    }
}
