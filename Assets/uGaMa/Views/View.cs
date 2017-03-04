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

        Dictionary<object, object> dispatchKeys = new Dictionary<object, object>();

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
            Debug.Log("VIEW AWAKE");
            _gameManager = GameManager.Instance;
            _dispatcher = _gameManager.Dispatcher;
            OnRegister();
        }

        public void OnDestroy()
        {
            OnRemove();
            _dispatcher.RemoveAllListeners(this);
        }

        public virtual void OnRegister() { }
        public virtual void OnRemove() { }


        public Dictionary<object, object> DispatchKeys()
        {
            return dispatchKeys;
        }

        public void OnHandlerObserver(ObserverParam param, Action<ObserverParam> callBack)
        {
            callBack(param);
        }
    }
}
