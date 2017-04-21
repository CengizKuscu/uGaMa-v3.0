using uGaMa.Views;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Examples.Simple.Views
{
    public class SimpleView0 : View
    {
        public Text HelloWorldTxt;
        public Button GetSceneNameBtn;
        public Button ChangeSceneBtn;

        public override void OnRegister()
        {
            HelloWorldTxt.text = "Hello World!";
            
            GetSceneNameBtn.onClick.AddListener(GetSceneName);
            ChangeSceneBtn.onClick.AddListener(ChangeScene);
        }

        private void ChangeScene()
        {
            Dispatcher.Dispatch(AppEvent.LoadScene, "Simple1");
        }

        private void GetSceneName()
        {
            HelloWorldTxt.text += "\n" + SceneManager.GetActiveScene().name;
        }

        public override void OnRemove()
        {

        }
    }
}