using uGaMa.Command;
using uGaMa.Observer;
using UnityEngine;

namespace Examples.Simple.Commands
{
    public class SetUpCMD : Command
    {
        public override void Execute(ObserverParam notify)
        {
            Debug.Log("SetUpCMD is Running");

            gameManager.CommandMap.Bind(AppEvent.LoadScene).To<LoadSceneCMD>();

            Debug.Log("SetupCmd is Complete");
        }
    }
}