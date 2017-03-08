using System.Collections.Generic;

namespace uGaMa.Bind
{
    public interface IBinding
    {
        IBinding Bind(object obj);

        IBinding Bind<T>();

        IBinding To(object obj);

        IBinding To<T>();

        IBinding SingleRun();

        Dictionary<object, object> Binded { get; }

        Dictionary<object, bool> SingleRunList { get; }

        object Key { get; }
    }
}
