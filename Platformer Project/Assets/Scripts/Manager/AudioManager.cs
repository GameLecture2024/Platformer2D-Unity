using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    // 오디오소스 컴포넌트를 추가해주세요.
    public AudioSource[] sfx;
    public AudioSource[] bgm; // Length

    // 현재 실행하고 있는 BGMIndex
    public int bgmIndex = 0;
    // 이벤트 함수 Start , Update 조건문

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    public void Start()
    {
        // PlayBGM(bgmIndex);
        PlayRandomBGM();
    }

    public void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    PlayRandomBGM();
        //}
    }

    public void PlayBGM(int bgmIndex) // bgmIndex에 해당하는 BGM 실행하는 함수
    {
        bgm[bgmIndex].Play();
    }

    public void PlaySFX(int sfxIndex)
    {
        if(sfxIndex < sfx.Length) // sfx.Length 큰 수를 받으면 배열초과 에러가 발생한다.
        {
            sfx[sfxIndex].pitch = Random.Range(0.85f, 1.15f);
            sfx[sfxIndex].Play();
            Debug.Log($"{sfx[sfxIndex].name} 출력됨");
        }
    }

    private void StopBGM() // 현재 실행 중인 BGM을 멈추는 함수
    {
        for(int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }

        //foreach(var sound in bgm)
        //{
        //    sound.Stop();
        //}
    }

    private void StopSFX()
    {
        for (int i = 0; i < sfx.Length; i++)
        {
            sfx[i].Stop();
        }
    }

    public void PlayRandomSFX()
    {
        StopSFX();
        int randomIndex = Random.Range(0, sfx.Length);
        PlaySFX(randomIndex);
    }

    public void PlayRandomBGM()
    {
        // 이전에 실행된 BGM은 멈춰라.
        StopBGM();
        // bgmIndex가 랜덤한 값을 가지고, 그 값을 실행하면된다.
        int randomIndex = Random.Range(0, bgm.Length);
        PlayBGM(randomIndex);
    }
}
