using Examples.Simple;
using uGaMa.Extensions.Pooling;
using uGaMa.Observer;
using uGaMa.Views;
using UnityEngine;

public class TestView : View<TestView>
{
    private ObjectPool cubePool;
    private ObjectPool capsulePool;

    public GameObject poolCube;

    public GameObject poolCapsule;

    protected override void OnRegister()
    {
        cubePool = new ObjectPool("Cube", poolCube, 2, 5, false);
        capsulePool = new ObjectPool("Capsule", poolCapsule, 5, 10, false);
        Dispatcher.AddListener(this, AppEvent.SetUp, OnSetUp);
    }

    private void OnSetUp(ObserverParam obj)
    {
        Debug.Log("OnSETUP");
    }

    protected override void OnRemove()
    {
        Dispatcher.RemoveAllListeners(this);
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical();

        GUILayout.Space(20);

        if (GUILayout.Button("STOP"))
        {
            MusicPlayerAdapter.StopMusic();
        }

        GUILayout.Space(20);

        if (GUILayout.Button("MUSIC 0"))
        {
            MusicPlayerAdapter.ChangeMusic("Dreamy");
        }

        GUILayout.Space(20);

        if (GUILayout.Button("MUSIC 1"))
        {
            MusicPlayerAdapter.ChangeMusic("Drum");
        }

        GUILayout.Space(20);

        if (GUILayout.Button("DESTROY"))
        {
            MusicPlayerAdapter.RemoveMusicPlayer();
        }

        GUILayout.Space(20);

        if (GUILayout.Button("POOL"))
        {
            cubePool.GetObject();
        }

        GUILayout.Space(20);

        if (GUILayout.Button("SHIRINK"))
        {
            cubePool.Shrink();
        }

        GUILayout.Space(20);

        if (GUILayout.Button("REMOVE ALL POOL"))
        {
            cubePool.RemoveAllObjects();
        }



        GUILayout.EndVertical();
    }
}
