using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private static AudioController _instance;
    public static AudioController Instance
    {
        get
        {
            if (_instance is null)
            {
                Debug.LogError("FALTA AUDIO CONTROLLER");
            }
            return _instance;
        }
    }
    [Header("Audio Sources")]
    [SerializeField]
    private AudioSource backgroundMusic;
    [SerializeField]
    private AudioSource backgroundMusic2;
    [SerializeField]
    private AudioSource fxSounds;
    [SerializeField]
    private AudioSource ambientMusic;

    [Header("Background Music Clips")]
    [SerializeField]
    AudioClip menuIntroBGClip;
    [SerializeField]
    AudioClip menuLoopBGClip;
    [SerializeField]
    AudioClip gameplayBGClip;
    [SerializeField]
    AudioClip gameOverBGClip;
    [SerializeField]
    AudioClip ambientGameplayBGClip;

    [Header("FX Sounds Clips")]
    [SerializeField]
    AudioClip hoeFXClip;
    [SerializeField]
    AudioClip shotgunFXClip;
    [SerializeField]
    AudioClip scarecrowFXClip;
    [SerializeField]
    AudioClip pickaxeFXClip;
    [SerializeField]
    AudioClip shovelFXClip;
    [SerializeField]
    AudioClip sprinklerFXClip;
    //[SerializeField]
    //AudioClip drawCardFXClip;
    [SerializeField]
    AudioClip sproutFXClip;
    [SerializeField]
    AudioClip incorrectTileFXClip;
    [SerializeField]
    AudioClip gameOverFXClip;

    [Header("VOLUMES BG Music")]
    [SerializeField]
    float menuIntroBGVolume = 0.1f;
    [SerializeField]
    float menuLoopBGVolume = 0.1f;
    [SerializeField]
    float gameplayBGVolume = 0.1f;
    [SerializeField]
    float gameOverBGVolume = 0.1f;
    [SerializeField]
    float pauseBGVolume = 0.1f;
    [SerializeField]
    float ambientGameplayBGVolume = 0.1f;
    [SerializeField]
    float ambientPauseBGVolume = 0.1f;

    [Header("VOLUMES FX Sounds")]
    [SerializeField]
    float hoeFXVolume = 0.3f;
    [SerializeField]
    float shotgunFXVolume = 0.3f;
    [SerializeField]
    float scarecrowFXVolume = 0.3f;
    [SerializeField]
    float pickaxeFXVolume = 0.3f;
    [SerializeField]
    float shovelFXVolume = 0.3f;
    [SerializeField]
    float sprinklerFXVolume = 0.3f;
    //[SerializeField]
    //float drawCardFXVolume = 0.3f;
    [SerializeField]
    float sproutFXVolume = 0.3f;
    [SerializeField]
    float incorrectTileFXVolume = 0.3f;
    [SerializeField]
    float gameOverFXVolume = 0.3f;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        if (backgroundMusic == null) backgroundMusic = transform.GetChild(0).GetComponent<AudioSource>();
        if (fxSounds == null) fxSounds = transform.GetChild(1).GetComponent<AudioSource>();
        fxSounds.loop = false;
        backgroundMusic.loop = false;
        backgroundMusic2.loop = false;
        ambientMusic.loop = true;
    }

    public void PlayMenuBGMusic()
    {
        backgroundMusic.Stop();
        backgroundMusic2.Stop();
        ambientMusic.Stop();

        backgroundMusic.clip = menuIntroBGClip;
        backgroundMusic.volume = menuIntroBGVolume;

        backgroundMusic2.clip = menuLoopBGClip;
        backgroundMusic2.volume = menuLoopBGVolume;

        backgroundMusic.Play();
        backgroundMusic2.PlayDelayed(menuIntroBGClip.length);
        backgroundMusic.loop = false;
        backgroundMusic2.loop = true;
        
    }

    public void PlayGameplayBGMusic()
    {
        backgroundMusic.Stop();
        backgroundMusic2.Stop();
        ambientMusic.Stop();

        backgroundMusic.clip = gameplayBGClip;
        backgroundMusic.volume = gameplayBGVolume;
        ambientMusic.clip = ambientGameplayBGClip;
        ambientMusic.volume = ambientGameplayBGVolume;
        backgroundMusic2.clip = gameplayBGClip;
        backgroundMusic2.volume = gameplayBGVolume;

        ambientMusic.Play();
        backgroundMusic.Play();
        backgroundMusic2.PlayDelayed(gameplayBGClip.length-3.5f);
        backgroundMusic.loop = false;
        backgroundMusic2.loop = true;
        ambientMusic.loop = true;
    }

    //private IEnumerator StartMenuLoop()
    //{
    //    yield return new WaitForSeconds(backgroundMusic.clip.length);
    //    if (backgroundMusic.clip == menuIntroBGClip)
    //    {
    //        backgroundMusic.clip = menuLoopBGClip;
    //        backgroundMusic.volume = menuLoopBGVolume;
    //        backgroundMusic.Play();
    //        backgroundMusic.loop = true;
    //        StopCoroutine("StartMenuLoop");
    //    }
    //    else StopCoroutine("StartMenuLoop");
    //}

    //private IEnumerator StartGameplayLoop()
    //{
    //    yield return new WaitForSeconds(backgroundMusic.clip.length-3.5f);
    //    //backgroundMusic.Stop();
    //    if (backgroundMusic.clip == gameplayBGClip && !backgroundMusic2.loop)
    //    {
    //        backgroundMusic2.clip = gameplayBGClip;
    //        backgroundMusic2.volume = gameplayBGVolume;
    //        backgroundMusic2.Play();
    //        backgroundMusic2.loop = true;
    //        StopCoroutine("StartGameplayLoop");
    //    }
    //    else StopCoroutine("StartGameplayLoop");
    //}



    public void SetPauseMusic(bool pause)
    {
        if (pause)
        {
            backgroundMusic.volume = pauseBGVolume;
            backgroundMusic2.volume = pauseBGVolume;
            ambientMusic.volume = ambientPauseBGVolume;
        }
        else
        {
            backgroundMusic.volume = gameplayBGVolume;
            backgroundMusic2.volume = gameplayBGVolume;
            ambientMusic.volume = ambientGameplayBGVolume;
        }
    }

    public void PlayGameOverBGMusic()
    {
        backgroundMusic.Stop();
        backgroundMusic2.Stop();
        ambientMusic.Stop();

        backgroundMusic.clip = gameOverBGClip;
        backgroundMusic.volume = gameOverBGVolume;
        backgroundMusic.Play();
        backgroundMusic.loop = false;
    }

    //public void PlayDrawCardSound() { PlaySound(drawCardFXClip, drawCardFXVolume); }
    public void PlayPickaxeOnRockSound() { PlaySound(pickaxeFXClip, pickaxeFXVolume); }
    public void PlayHoeOnSoilSound() { PlaySound(hoeFXClip, hoeFXVolume); }
    public void PlaySproutSound() { PlaySound(sproutFXClip, sproutFXVolume); }
    public void PlaySpinkleSound() { PlaySound(sprinklerFXClip, sprinklerFXVolume); }
    public void PlayShovelOnCarrotSound() { PlaySound(shovelFXClip, shovelFXVolume); }
    public void PlayShotgunOnBirdSound() { PlaySound(shotgunFXClip, shotgunFXVolume); }
    public void PlayScarecrowSound() { PlaySound(scarecrowFXClip, scarecrowFXVolume); }
    public void PlayIncorrectSound() { PlaySound(incorrectTileFXClip, incorrectTileFXVolume); }
    public void PlayGameOverSound() { PlaySound(gameOverFXClip, gameOverFXVolume); }

    private void PlaySound(AudioClip clip, float volume)
    {
        fxSounds.Stop();
        fxSounds.clip = fxSounds.clip = clip;
        fxSounds.volume = volume;
        fxSounds.Play();
    }
}
