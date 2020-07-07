using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using uGaMa.Core;
using uGaMa.Extensions.MenuSystem;
using uGaMa.Observer;
using uGaMa.Views;
using UnityEngine;

namespace uGaMa.Extensions.MenuSystem
{
    public abstract class AbsMenu : MonoBehaviour, IObserver
    {
        public string menuName;
        public string prevMenuName;
        
        List<object> dispatchKeys = new List<object>();

        [SerializeField] private bool isPopup;

        [SerializeField] private bool autoRemoveDispatches;

        [SerializeField] private bool destroyOnClose;

        public bool IsPopup
        {
            get { return isPopup; }
        }

        public bool AutoRemoveDispatches
        {
            get { return autoRemoveDispatches; }
        }

        public bool DestroyOnClose
        {
            get { return destroyOnClose; }
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

        protected abstract void OnShowBefore(object param);
        protected abstract void OnShowAfter(object param);
        protected abstract void OnHideBefore(object param);

        protected abstract void OnHideAfter(object param);    
    }


    public partial class MenuBase : AbsMenu
    {
        protected override void OnShowBefore(object param)
        {
        }

        protected override void OnShowAfter(object param)
        {
        }

        protected override void OnHideBefore(object param)
        {
        }

        protected override void OnHideAfter(object param)
        {
        }

        public void Open(object param = null)
        {
            OnShowBefore(param);
            gameObject.SetActive(true);
            OnShowAfter(param);
        }

        public void Close(object param = null)
        {
            RemoveDispatches();
            OnHideBefore(param);
            gameObject.SetActive(false);
            OnHideAfter(param);
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