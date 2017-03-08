using System.Collections.Generic;

namespace uGaMa.Bind
{
    public class Binding : IBinding
    {
        public Binder.BindingResolver Resolver;

        public Binding() : this(null)
        {
            Binded = new Dictionary<object, object>();
            SingleRunList = new Dictionary<object, bool>();
        }

        public Binding(Binder.BindingResolver resolver)
        {
            this.Resolver = resolver;
            Binded = new Dictionary<object, object>();
            SingleRunList = new Dictionary<object, bool>();
        }

        public Dictionary<object, object> Binded { get; private set; }

        public Dictionary<object, bool> SingleRunList { get; private set; }

        public object Key { get; private set; }

        public IBinding Bind(object obj)
        {
            Key = obj;
            return this;
        }

        public IBinding Bind<T>() { return Bind(typeof(T)); }

        public IBinding To(object obj)
        {
            Binded.Add(obj, obj);
            if (Resolver != null)
            {
                Resolver(this);
            }
            return this;
        }

        public IBinding To<T>() { return To(typeof(T)); }

        public IBinding SingleRun()
        {
            if (!SingleRunList.ContainsKey(this.Key))
            {
                SingleRunList.Add(this.Key, false);
            }
            return this;
        }
    }
}
