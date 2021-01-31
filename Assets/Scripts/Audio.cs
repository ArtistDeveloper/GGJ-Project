using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioClip audioJump;     //EtcSound
    public AudioClip audioWalking;   
    public AudioClip audioSpeaking;
    public AudioClip audioDie;
    public AudioClip audioCellConversion;
    public AudioClip audioNextStage;
    public AudioClip audioPressNoEndButton;
    public AudioClip audioButtonClick;
    public AudioClip audioBackground;
    AudioSource audioSource;

    void Awake()
    {
        this.audioSource = GetComponent<AudioSource>();
    }
}
