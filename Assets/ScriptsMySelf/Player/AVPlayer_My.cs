using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AVPlayer_My : MonoBehaviour
{
    [SerializeField] ParticleSystem particleEffect;//粒子效果
    [SerializeField] AudioSource audioEffect;//声音


    //初始化
    private void Reset()
    {
        particleEffect = GetComponent<ParticleSystem>();
        audioEffect = GetComponent<AudioSource>();
    }

    public void Play()
    {
        if(particleEffect!=null)
        {
            particleEffect.Play(true);//粒子效果播放
        }
        if(audioEffect!=null)
        {
            audioEffect.Play();//播放音效
        }
    }
}
