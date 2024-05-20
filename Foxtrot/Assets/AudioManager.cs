using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("----------Audio Sources-----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource Repeater;
    // Start is called before the first frame update

    [Header("----------Audio Clips-----------")]
    public AudioClip wind;
    public AudioClip walking;
    public AudioClip forwardAttack;
    public AudioClip jump;
    public AudioClip upAttack;
    public AudioClip backwardsAttack;
    public AudioClip downAttack;
    public AudioClip treeBackground;
    public AudioClip waterfall;
    public AudioClip background;
    public AudioClip landing;


    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }


    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlaySFXOnce(AudioClip clip)
    {
        SFXSource.clip = clip;
        SFXSource.Play();
    }   

    public void PlayRepeater(AudioClip clip)
    {
        Repeater.clip = clip;
        Repeater.Play();
    }


}
