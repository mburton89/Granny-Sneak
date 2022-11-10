using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource selectSound;
    public AudioSource moveSound;

    private void Awake()
    {
        Instance = this;
    }
}
