using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource selectSound;
    public AudioSource moveSound;
    public AudioSource mattQuoter;

    public List<AudioClip> audioClips;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayRandomQuote()
    {
        int rand = Random.Range(0, audioClips.Count - 1);

        mattQuoter.clip = audioClips[rand];
        mattQuoter.Play();
    }
}
