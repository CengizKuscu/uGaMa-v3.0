using System;
using System.Collections.Generic;
using System.Linq;
using uGaMa.Core;

namespace uGaMa.Observer
{
    public class DispatchManager : Singleton<DispatchManager>
    {
        private GameManager gameManager
        {
            get { return GameManager.Instance; }
        }

        private Dictionary<object, Dictionary<Action<ObserverParam>, IObserver>> _dispatchList; 

        public Dictionary<object, Dictionary<Action<ObserverParam>, IObserver>> DispatchList
        {
            get
            {
                if(Instance._dispatchList == null)
                    Instance._dispatchList = new Dictionary<object, Dictionary<Action<ObserverParam>, IObserver>>();

                return Instance._dispatchList;
            }
        }

        public void AddListener(IObserver obj, object dispatchKey, Action<ObserverParam> callBack)
        {
            if(obj.DispatchKeys.Find(s=> s == dispatchKey) == null)
            {
                obj.DispatchKeys.Add(dispatchKey);
            }

            if(!DispatchList.ContainsKey(dispatchKey))
            {
                DispatchList.Add(dispatchKey, new Dictionary<Action<ObserverParam>, IObserver> { { callBack, obj } });
            }
            else
            {
                DispatchList[dispatchKey].Add(callBack, obj);
            }
        }

        public void RemoveListener(IObserver obj, object dispatchKey, Action<ObserverParam> callback)
        {
            if (ApplicationIsQuitting) return;
            
            var actions = DispatchList[dispatchKey];
            if (actions == null) return;
            for (int i = 0; i < actions.Count; i++)
            {
                if(actions.Keys.ElementAt(i) == callback && actions.Values.ElementAt(i) == obj)
                {
                    actions.Remove(actions.Keys.ElementAt(i));
                }
            }
            if (actions.Count == 0)
            {
                DispatchList.Remove(dispatchKey);
            }

            obj.DispatchKeys.Remove(obj.DispatchKeys.Find(s => s == dispatchKey));
        }

        public void RemoveAllListeners(IObserver obj)
        {
            if (GameManager.ApplicationIsQuitting) return;
            
            foreach (var dispatchKey in obj.DispatchKeys)
            {
                var actions = DispatchList[dispatchKey];
                if (!actions.ContainsValue(obj)) continue;
                for (int i = 0; i < actions.Count; i++)
                {
                    if (actions.Values.ElementAt(i) == obj)
                    {
                        actions.Remove(actions.Keys.ElementAt(i));
                    }
                }

                if(actions.Count == 0)
                {
                    DispatchList.Remove(dispatchKey);
                }
            }
            obj.DispatchKeys.Clear();
        }

        public void Dispatch(object dispatchKey, object dispatchParam, object dispatchMsg)
        {
            var notify = new ObserverParam(dispatchKey, dispatchParam, dispatchMsg);
            gameManager.CommandMap.ExecuteCommand(notify);
            SendNotifyToObject(notify);
        }

        public void Dispatch(object dispatchKey, object dispatchParam)
        {
            var notify = new ObserverParam(dispatchKey, dispatchParam, null);
            gameManager.CommandMap.ExecuteCommand(notify);
            SendNotifyToObject(notify);
        }

        public void Dispatch(object dispatchKey)
        {
            var notify = new ObserverParam(dispatchKey, null, null);
            gameManager.CommandMap.ExecuteCommand(notify);
            SendNotifyToObject(notify);
        }

        private void SendNotifyToObject(ObserverParam notify)
        {
            if (!DispatchList.ContainsKey(notify.Key)) return;
            var actions = DispatchList[notify.Key];

            for (var i = 0; i < actions.Count; i++)
            {
                var tmpBehavior = actions.Values.ElementAt(i);
                tmpBehavior.OnHandlerObserver(notify, actions.Keys.ElementAt(i));
            }
        }

    }
}
