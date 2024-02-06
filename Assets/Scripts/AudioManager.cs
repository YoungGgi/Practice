using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("# BGM")]
    [SerializeField]
    private AudioClip bgmClip;
    [SerializeField]
    private float bgmVolume;
    AudioSource bgmPlayer;
    AudioHighPassFilter bgmEffect;    // AudioHighPassFilter = 오디오 필터, 주파수가 높은 사운드만 출력하는 컴포넌트

    [Header("# SFX")]
    [SerializeField]
    private AudioClip[] sfxClip;
    [SerializeField]
    private float sfxVolume;
    [SerializeField]
    private int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;

    public enum Sfx { Dead, Hit, LevelUp = 3, Lose, Melee, Range = 7, Select, Win }

    private void Awake() 
    {
        instance = this;

        Init();
    }

    void Init()
    {
        // 배경음 플레이어 초기화
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;
        bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();

        // 효과음 플레이어 초기화
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for(int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].bypassListenerEffects = true;             // bypassListnenerEffects = AudioHighPassFilter의 영향을 받지 않음
            sfxPlayers[i].volume = sfxVolume;

        }

    }

    public void PlayBgm(bool isPlay)
    {
        if(isPlay)
        {
            bgmPlayer.Play();
        }
        else
        {
            bgmPlayer.Stop();
        }
    }

    public void EffectBgm(bool isPlay)
    {
        bgmEffect.enabled = isPlay;
    }

    public void PlaySfx(Sfx sfx)
    {
        for(int i = 0; i < sfxPlayers.Length; i++)
        {
            // 현재 있는 sfxPlayers 개수만큼 순회
            int loopIndex = (i + channelIndex) % sfxPlayers.Length;
            
            // 이미 재생중인 효과음이 있을 시 건너뜀
            if(sfxPlayers[loopIndex].isPlaying)
            {
                continue;
            }

            // 중복되는 종류의 효과음이 있을 시 랜덤으로 츨력 (Hit, Melee)
            int randomIndex = 0;
            if(sfx == Sfx.Hit || sfx == Sfx.Melee)
            {
                randomIndex = Random.Range(0, 2);
            }

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClip[(int)sfx + randomIndex];
            sfxPlayers[loopIndex].Play();
            break;
        }
        
        
    }

}
