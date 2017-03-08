using System;
using System.Collections.Generic;


namespace uGaMa.Observer
{
    public interface IObserver
    {
        List<object> DispatchKeys { get; }

        void OnHandlerObserver(ObserverParam param, Action<ObserverParam> callBack);
    }
}
