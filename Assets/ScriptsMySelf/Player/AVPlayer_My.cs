using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AVPlayer_My : MonoBehaviour
{
    [SerializeField] ParticleSystem particleEffect;//����Ч��
    [SerializeField] AudioSource audioEffect;//����


    //��ʼ��
    private void Reset()
    {
        particleEffect = GetComponent<ParticleSystem>();
        audioEffect = GetComponent<AudioSource>();
    }

    public void Play()
    {
        if(particleEffect!=null)
        {
            particleEffect.Play(true);//����Ч������
        }
        if(audioEffect!=null)
        {
            audioEffect.Play();//������Ч
        }
    }
}
