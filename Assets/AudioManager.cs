using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioClip defaultClip; 
    public AudioSource audioSource;
    public static AudioManager instance; 
    private void Awake()
    {
        audioSource.loop = true;
        PlayDefaultClip();
    }

    void PlayDefaultClip()
    {
        if (defaultClip != null)
        {
            audioSource.clip = defaultClip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Default audio clip is not set!");
        }
    }
}
