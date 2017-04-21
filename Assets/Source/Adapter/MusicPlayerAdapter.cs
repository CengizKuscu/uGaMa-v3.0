
public class MusicPlayerAdapter
{
    public static void StopMusic()
    {
        MusicPlayer.Instance.StopMusic();
    }

    public static void ChangeMusic(string path)
    {
        MusicPlayer.Instance.ChangeMusic(path);
    }

    public static void RemoveMusicPlayer()
    {
        MusicPlayer.Instance.DestroyPlayer();
    }
}
