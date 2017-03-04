using System;
using System.Collections.Generic;


namespace uGaMa.Observer
{
    public interface IObserver
    {
        Dictionary<object, object> DispatchKeys();

        void OnHandlerObserver(ObserverParam param, Action<ObserverParam> callBack);
    }
}
