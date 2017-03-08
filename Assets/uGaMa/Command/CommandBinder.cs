using System;
using uGaMa.Bind;
using uGaMa.Observer;

namespace uGaMa.Command
{
    public class CommandBinder : Binder
    {
        public CommandBinder() : base()
        {

        }

        protected internal void ExecuteCommand(ObserverParam param)
        {
            var binding = GetBind(param.Key);

            if (binding == null)
            {
                return;
            }

            if (binding.SingleRunList.ContainsKey(param.Key))
            {
                if (binding.SingleRunList[param.Key]) return;
            }

            var binded = binding.Binded;

            if (binded == null) return;

            foreach (var pair in binded)
            {
                var cmd = pair.Value as Type;
                if (cmd == null) continue;
                var command = (ICommand)Activator.CreateInstance(cmd);
                command.Execute(param);
            }

            if (binding.SingleRunList.ContainsKey(param.Key))
            {
                if (!binding.SingleRunList[param.Key])
                {
                    binding.SingleRunList[param.Key] = true;
                }
            }
        }
    }
}
