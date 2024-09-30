using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioMixer audioMixer;
    public AudioSource bgmSource;
    public AudioClip bgmClip;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBGM(bgmClip);
    }

    public void PlaySFX(string sfxName, AudioClip clip)
    {
        GameObject sfx = new GameObject(sfxName);
        AudioSource audioSource = sfx.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0];
        audioSource.clip = clip;
        audioSource.Play();

        Destroy(sfx, clip.length);
    }

    public void PlayBGM(AudioClip bgmClip)
    {
        bgmSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("BGM")[0];
        bgmSource.clip = bgmClip;
        bgmSource.loop = true;
        //bgmSource.volume = 0.1f;
        bgmSource.Play();
    }
}
