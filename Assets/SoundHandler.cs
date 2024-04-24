using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    public AudioClip[] soundClips;
    public AudioSource audioSource;

    public void PlaySound(string soundName){
        foreach(AudioClip clip in soundClips){
            if (clip.name == soundName){
                audioSource.Stop();
                Debug.Log("Playing sound:     "+ clip);
                audioSource.PlayOneShot(clip);
                return;
            }
        }
    }


}
