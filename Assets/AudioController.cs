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

    [Header("Background Music Clips")]
    [SerializeField]
    AudioClip menuIntroBGClip;
    [SerializeField]
    AudioClip menuLoopBGClip;
    [SerializeField]
    AudioClip gameplayBGClip;
    [SerializeField]
    AudioClip gameOverBGClip;

    [Header("FX Sounds Clips")]
    [SerializeField]
    AudioClip hoeFXClip;
    [SerializeField]
    AudioClip shuffleDeckFXClip;
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
    [SerializeField]
    AudioClip drawCardFXClip;
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

    [Header("VOLUMES FX Sounds")]
    [SerializeField]
    float hoeFXVolume = 0.3f;
    [SerializeField]
    float shuffleDeckFXVolume = 0.3f;
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
    [SerializeField]
    float drawCardFXVolume = 0.3f;
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
    }

    public void PlayMenuBGMusic()
    {
        backgroundMusic.Stop();
        backgroundMusic.clip = menuIntroBGClip;
        backgroundMusic.volume = menuIntroBGVolume;
        backgroundMusic.Play();
        backgroundMusic.loop = false;
        StartCoroutine("StopMenuIntro");
        StartCoroutine("StartMenuLoop");
    }

    private IEnumerator StartGameplayLoop()
    {
        yield return new WaitForSeconds(backgroundMusic.clip.length-3.5f);
        //backgroundMusic.Stop();
        if (backgroundMusic.clip == gameplayBGClip && !backgroundMusic2.loop)
        {
            backgroundMusic2.clip = gameplayBGClip;
            backgroundMusic2.volume = gameplayBGVolume;
            backgroundMusic2.Play();
            backgroundMusic2.loop = true;
            StopCoroutine("StartGameplayLoop");
        }
        else StopCoroutine("StartGameplayLoop");
    }

    private IEnumerator StartMenuLoop()
    {
        yield return new WaitForSeconds(backgroundMusic.clip.length-1f);
        if (backgroundMusic.clip == menuIntroBGClip)
        {
            backgroundMusic2.clip = menuLoopBGClip;
            backgroundMusic2.volume = menuLoopBGVolume;
            backgroundMusic2.Play();
            backgroundMusic2.loop = true;
            StopCoroutine("StartMenuLoop");
        }
        else StopCoroutine("StartMenuLoop");
    }

    private IEnumerator StopMenuIntro()
    {
        yield return new WaitForSeconds(backgroundMusic.clip.length);
        backgroundMusic.Stop();
        StopCoroutine("StopMenuIntro");
    }

        public void PlayGameplayBGMusic()
    {
        backgroundMusic2.Stop();
        backgroundMusic.Stop();
        backgroundMusic2.loop = false;
        backgroundMusic.clip = gameplayBGClip;
        backgroundMusic.volume = gameplayBGVolume;
        backgroundMusic.Play();
        backgroundMusic.loop = false;
        StartCoroutine("StartGameplayLoop");
    }

    public void SetPauseMusic(bool pause)
    {
        if (pause) backgroundMusic.volume = pauseBGVolume;
        else backgroundMusic.volume = gameplayBGVolume;
    }

    public void PlayGameOverBGMusic()
    {
        backgroundMusic.Stop();
        backgroundMusic.clip = gameOverBGClip;
        backgroundMusic.volume = gameOverBGVolume;
        backgroundMusic.Play();
        backgroundMusic.loop = false;
    }

    public void PlayDrawCardSound() { PlaySound(drawCardFXClip, drawCardFXVolume); }
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
