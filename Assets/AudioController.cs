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
    private AudioSource fxSounds;

    [Header("Background Music Clips")]
    [SerializeField]
    AudioClip menuIntroBMClip;
    [SerializeField]
    AudioClip menuLoopBMClip;
    [SerializeField]
    AudioClip gameplayBMClip;
    [SerializeField]
    AudioClip gameOverBMClip;

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
    }

    public void PlayMenuBGMusic()
    {
        backgroundMusic.Stop();
        backgroundMusic.clip = menuIntroBMClip;
        backgroundMusic.Play();
        backgroundMusic.loop = false;
        StartCoroutine("StartMenuLoop");
    }

    private IEnumerator StartMenuLoop()
    {
        yield return new WaitForSeconds(backgroundMusic.clip.length-0.2f);
        backgroundMusic.Stop();
        backgroundMusic.clip = menuLoopBMClip;
        backgroundMusic.Play();
        backgroundMusic.loop = true;
        StopCoroutine("StartMenuLoop");
    }

    public void PlayGameplayBGMusic()
    {
        backgroundMusic.Stop();
        backgroundMusic.clip = gameplayBMClip;
        backgroundMusic.Play();
        backgroundMusic.loop = true;
    }

    public void PlayDrawCardSound() { PlaySound(drawCardFXClip); }
    public void PlayPickaxeOnRockSound() { PlaySound(pickaxeFXClip); }
    public void PlayHoeOnSoilSound() { PlaySound(hoeFXClip); }
    public void PlaySproutSound() { PlaySound(sproutFXClip); }
    public void PlaySpinkleSound() { PlaySound(sprinklerFXClip); }
    public void PlayShovelOnCarrotSound() { PlaySound(shovelFXClip); }
    public void PlayShotgunOnBirdSound() { PlaySound(shotgunFXClip); }
    public void PlayScarecrowSound() { PlaySound(scarecrowFXClip); }
    public void PlayIncorrectSound() { PlaySound(incorrectTileFXClip); }

    private void PlaySound(AudioClip clip)
    {
        fxSounds.Stop();
        fxSounds.clip = fxSounds.clip = clip;
        fxSounds.Play();
    }
}
