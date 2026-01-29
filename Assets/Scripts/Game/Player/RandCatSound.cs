using UnityEngine;

public class RandCatSound : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private AudioClip[] catSounds;
    [SerializeField]private AudioSource AudioSourceCat;
    [SerializeField] private float minPitch = 0.9f;
    [SerializeField] private float maxPitch = 1.1f;
    [SerializeField] private float delayCatSound = 2f;

    void Start()
    {
        InvokeRepeating("PlayRandomCatSound", delayCatSound, delayCatSound);
    }

    void PlayRandomCatSound()
    {
        if(MouseController.instance.menang || MouseController.instance.kalah) return;
        if (catSounds.Length > 0 && AudioSourceCat != null)
        {
            int randomIndex = Random.Range(0, catSounds.Length);
            AudioSourceCat.clip = catSounds[randomIndex];
            AudioSourceCat.pitch = Random.Range(minPitch, maxPitch);
            AudioSourceCat.Stop();
            AudioSourceCat.Play();
        }
    }
}