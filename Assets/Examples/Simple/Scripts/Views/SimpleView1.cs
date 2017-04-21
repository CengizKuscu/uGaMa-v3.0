using uGaMa.Views;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Examples.Simple.Views
{
    public class SimpleView1 : View
    {
        public GameObject CubeObj;

        public Text SceneNameTxt;
        public Button ChangeSceneBtn;

        public override void OnRegister()
        {
            SceneNameTxt.text = "Changed Scene\n"+SceneManager.GetActiveScene().name;

            ChangeSceneBtn.onClick.AddListener(ChangeScene);
        }

        private void ChangeScene()
        {
            Dispatcher.Dispatch(AppEvent.LoadScene, "Simple0");
        }

        public override void OnRemove()
        {

        }
    }
}