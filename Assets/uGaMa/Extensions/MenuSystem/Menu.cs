using System;
using System.Collections.Generic;
using uGaMa.Observer;
using UnityEngine;

namespace uGaMa.Extensions.MenuSystem
{
    public abstract class AbsMenu : MonoBehaviour
    {
        //public abstract T menuType
        
        protected virtual void Awake()
        {
            gameObject.SetActive(false);
            OnRegister();
        }

        protected virtual void OnDestroy()
        {
            OnRemove();
        }

        protected virtual void OnRegister(){}

        protected virtual void OnRemove(){}

        public virtual void OnShowBefore(object param){}

        public virtual void OnShowAfter(object param){}

        public virtual void OnHideBefore(object param){}

        public virtual void OnHideAfter(object param){}

        public virtual void Open(object param = null)
        {
            OnShowBefore(param);
            gameObject.SetActive(true);
            OnShowAfter(param);
            
        }


        public virtual void Close(object param = null)
        {
            OnHideBefore(param);
            gameObject.SetActive(false);
            OnHideAfter(param);
        }

    }

    public abstract partial class Menu : AbsMenu, IObserver
    {
        
        private List<object> dispatchKeys = new List<object>();

        public List<object> DispatchKeys
        {
            get { return dispatchKeys; }
        }
        
        public void OnHandlerObserver(ObserverParam param, Action<ObserverParam> callBack)
        {
            callBack(param);
        }

        
        
    }
}