using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManger : MonoBehaviour
{
    public void playHitSound(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
} 
