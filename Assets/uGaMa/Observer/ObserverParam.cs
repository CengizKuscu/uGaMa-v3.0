namespace uGaMa.Observer
{
    public class ObserverParam
    {
        public ObserverParam(object key, object data, object msg)
        {
            this.Key = key;
            this.Data = data;
            this.Msg = msg;
        }

        public object Key { get; private set; }

        public object Data { get; private set; }

        public object Msg { get; private set; }
    }
}
