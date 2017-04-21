using uGaMa.Command;
using uGaMa.Observer;
using UnityEngine.SceneManagement;

namespace Examples.Simple.Commands
{
    public class LoadSceneCMD : Command
    {
        public override void Execute(ObserverParam notify)
        {
            string sceneName = notify.Data as string;

            SceneManager.LoadScene(sceneName);
        }
    }
}