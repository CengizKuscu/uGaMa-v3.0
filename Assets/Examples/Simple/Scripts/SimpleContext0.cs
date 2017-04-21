using Examples.Simple.Commands;
using uGaMa.Core;

namespace Examples.Simple
{
    public class SimpleContext0 : Context
    {
        public override void OnRegister()
        {
            CommandMap.Bind(AppEvent.SetUp).To<SetUpCMD>().SingleRun();

            Dispatcher.Dispatch(AppEvent.SetUp);
        }

        public override void OnRemove()
        {

        }
    }
}