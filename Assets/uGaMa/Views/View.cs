using System;
using System.Collections.Generic;
using uGaMa.Core;
using uGaMa.Observer;
using uGaMa.Views;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace uGaMa.Views
{
    public abstract class AbsView : MonoBehaviour, IObserver
    {
        List<object> dispatchKeys = new List<object>();

        [SerializeField] private bool autoRemoveDispatches;

        public bool AutoRemoveDispatches
        {
            get { return autoRemoveDispatches; }
        }

        public List<object> DispatchKeys
        {
            get { return dispatchKeys; }
        }

        public GameManager gameManager
        {
            get { return GameManager.Instance; }
        }
        
        public void OnHandlerObserver(ObserverParam param, Action<ObserverParam> callBack)
        {
            callBack(param);
        }

        public DispatchManager Dispatcher
        {
            get { return DispatchManager.Instance; }
        }

        protected virtual void Awake(){}
        
        protected virtual void OnEnable(){}
        
        protected virtual void OnDisable(){}
        
        protected virtual void OnDestroy(){}

        protected abstract void OnRegister();

        protected abstract void OnRemove();        
    }


    public class View : AbsView
    {
        protected override void Awake()
        {
            OnRegister();
        }

        protected override void OnDestroy()
        {
            RemoveDispatches();
            OnRemove();
        }

        protected override void OnDisable()
        {
            RemoveDispatches();
            OnRemove();
        }

        protected override void OnRegister()
        {
        }

        protected override void OnRemove()
        {
        }

        private void RemoveDispatches()
        {
            if (DispatchManager.IsApplicationQuit)
                return;

            if (AutoRemoveDispatches && DispatchKeys.Count > 0)
            {
                Dispatcher.RemoveAllListeners(this);
            }
        }

        public void RemoveAllDispatches()
        {
            if (DispatchManager.IsApplicationQuit)
                return;
            Dispatcher.RemoveAllListeners(this);
        }
    }   
}