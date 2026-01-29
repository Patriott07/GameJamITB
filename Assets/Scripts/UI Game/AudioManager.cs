using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource winSource;
    [SerializeField] private AudioSource loseSource,objectSource, catScreamSource;
   

    [Header("Clip Source")]
    [SerializeField] public AudioClip pecahObj;
    [SerializeField] public AudioClip catNabrakRobot, catDorongBox, catScream, openPaper;

    public static AudioManager Instance;
    void Start()
    {
        Instance = this;
    }

    public void PlayWinAudio()
    {
        winSource.Stop();
        winSource.Play();
    }

    public void PlayLoseAudio()
    {
        loseSource.Stop();
        loseSource.Play();
    }

    public void PlayObjectAudio(AudioClip clip)
    {
        objectSource.Stop();
        objectSource.pitch = Random.Range(0.9f, 1.1f);
        objectSource.clip = clip;
        objectSource.Play();
    }

     public void CatScream()
    {
        catScreamSource.Stop();
        catScreamSource.pitch = Random.Range(0.9f, 1.1f);
        catScreamSource.Play();
    }
}