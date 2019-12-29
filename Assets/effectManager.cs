using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectManager : MonoBehaviour
{
    AudioClip buttonSound, jumpSound, landingSound, machineSound, GameOverSound, ClearSound;
    [HideInInspector] public AudioSource audioSource;

    public static effectManager Instance = null;
    public float effectValue = 1.0f;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance == null)
            Instance = this;
        buttonSound = Resources.Load<AudioClip>("button");
        jumpSound = Resources.Load<AudioClip>("jump");
        landingSound = Resources.Load<AudioClip>("landing");
        machineSound = Resources.Load<AudioClip>("machine");
        GameOverSound = Resources.Load<AudioClip>("GameOver");
        ClearSound = Resources.Load<AudioClip>("Clear");
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.volume = effectValue;
    }


    public void PlaySound(string clip)
    {
        switch (clip)
        {
            case "button":
                audioSource.PlayOneShot(buttonSound);
                break;
            case "jump":
                audioSource.PlayOneShot(jumpSound);
                break;
            case "landing":
                audioSource.PlayOneShot(landingSound);
                break;
            case "machine":
                audioSource.PlayOneShot(machineSound);
                break;
            case "GameOver":
                audioSource.clip = GameOverSound;
                audioSource.Play();
                break;
            case "Clear":
                audioSource.clip = ClearSound;
                audioSource.Play();
                break;
        }
    }

    public void StopSound()
    {
        audioSource.Stop();
    }
}
