using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum SFXType
{
    // Player
    Step

}

public enum MusicType
{
    menuBackground,
    gameBackground,
    dieMenu,
    completeLevel,
}

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioSource _musicSource;

    [SerializeField] private List<SFXData> _sfxDatas = new List<SFXData>();
    [SerializeField] private List<MusicData> _musicDatas = new List<MusicData>();

    [SerializeField] private AudioMixer _audioMixer;

    private static AudioManager _instance;

    private void Awake()
    {
        
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        SetVolume();
        //GameControll.onChangeVolume += SetVolume;
        //DiedMenu.onStopAllSounds += StopAllSounds;
        //CompleteLevel.onAllSoundStop += StopAllSounds;
    }

    private void Update()
    {
        //if(GameObject.FindGameObjectWithTag("DiedMenu") == null)
        //{
        //    //DiedMenu.onStopAllSounds -= StopAllSounds;
        //}
    }

    private void PlaySFXInner(SFXType sfx)
    {
        var sfxData = _sfxDatas.Find(data => data.SFX == sfx);
        if (sfxData != null)
        {
            _sfxSource.PlayOneShot(sfxData.Audio);
        }
    }

    private void StopSFXInner()
    {
            _sfxSource.Stop();
    }

    private void StopAllSounds()
    {
        _sfxSource.Stop();
        _musicSource.Stop();
    }

    public static void StopSFX()
    {
        _instance.StopSFXInner();
    }

    private void PlayMusicInner(MusicType musicType)
    {
        var musicData = _musicDatas.Find(data => data.Type == musicType);
        if (musicData != null)
        {
            _musicSource.clip = musicData.Music;
            _musicSource.Play();
        }
    }

    public static void PlaySFX(SFXType sfx)
    {
        _instance.PlaySFXInner(sfx);
    }

    public static void PlayMusic(MusicType music)
    {
        _instance.PlayMusicInner(music);
    }

    private void SetVolume()
    {
        float sfx = PlayerPrefs.GetFloat("SFXVolume");
        float music = PlayerPrefs.GetFloat("MusicVolume");
        _audioMixer.SetFloat("SFX", Mathf.Log10(sfx)*20);
        _audioMixer.SetFloat("SOUND", Mathf.Log10(music)*20);
    }

}

[System.Serializable]
public class SFXData
{
    [SerializeField] private SFXType _sfx;
    [SerializeField] private AudioClip _audioClip;

    public SFXType SFX => _sfx;
    public AudioClip Audio => _audioClip;
      
}


[System.Serializable]
public class MusicData
{
    [SerializeField] private MusicType _musicType;
    [SerializeField] private AudioClip _clip;

    public MusicType Type => _musicType;
    public AudioClip Music => _clip;

}