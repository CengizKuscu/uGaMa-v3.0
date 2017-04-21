using uGaMa.Core;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : Singleton<MusicPlayer>
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.loop = true;
        _audioSource.volume = 0.5f;
    }

    public void StopMusic()
    {
        if (_audioSource != null)
        {
            if (_audioSource.isPlaying == true)
            {
                _audioSource.Stop();
            }
        }
    }

    public void ChangeMusic(string path)
    {
        _audioSource.clip = Resources.Load<AudioClip>(path);
        if (_audioSource.clip != null)
        {
            _audioSource.Play();
        }
        else
        {
            _audioSource.Stop();
        }
    }

    public void DestroyPlayer()
    {
        Destroy(gameObject);
    }

    private void OnApplicationQuit()
    {
        StopMusic();
    }

    private new void OnDestroy()
    {
        StopMusic();
    }
}
