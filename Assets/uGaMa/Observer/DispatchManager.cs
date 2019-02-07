using System;
using System.Collections.Generic;
using System.Linq;
using uGaMa.Command;
using uGaMa.Core;

namespace uGaMa.Observer
{
    public class DispatchManager : Singleton<DispatchManager>
    {
        private CommandBinder _commandManager;
        public Dictionary<object, Dictionary<Action<ObserverParam>, IObserver>> DispatchList;

        private void Awake()
        {
            _commandManager = new CommandBinder();
            DispatchList = new Dictionary<object, Dictionary<Action<ObserverParam>, IObserver>>();
        }
        
        public CommandBinder CommandMap
        {
            get
            {
                return _commandManager;
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
            if (IsApplicationQuit)
                return;
            
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
            if (IsApplicationQuit)
                return;
            
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
            CommandMap.ExecuteCommand(notify);
            SendNotifyToObject(notify);
        }

        public void Dispatch(object dispatchKey, object dispatchParam)
        {
            var notify = new ObserverParam(dispatchKey, dispatchParam, null);
            CommandMap.ExecuteCommand(notify);
            SendNotifyToObject(notify);
        }

        public void Dispatch(object dispatchKey)
        {
            var notify = new ObserverParam(dispatchKey, null, null);
            CommandMap.ExecuteCommand(notify);
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
