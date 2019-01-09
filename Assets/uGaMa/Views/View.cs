using System;
using System.Collections.Generic;
using uGaMa.Core;
using uGaMa.Observer;
using UnityEngine;

namespace uGaMa.Views
{
    public abstract class View<T> : View where T : View<T>, IObserver
    {
        protected virtual void Awake()
        {
            OnRegister();
        }

        protected virtual void OnDestroy()
        {
            OnRemove();
        }
        
        protected virtual void OnRegister(){}
        
        protected virtual void OnRemove(){}
    }
    
    public class View : MonoBehaviour, IObserver
    {
        List<object> dispatchKeys = new List<object>();
        
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

        protected DispatchManager Dispatcher
        {
            get { return DispatchManager.Instance; }
        }

        protected GameManager gameManager
        {
            get { return GameManager.Instance; }
        }
    }
}
