using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseCav : MonoBehaviour
{
    [Header("��������")]
    [SerializeField] Slider SliderBg;//����������������Slider
    [SerializeField] AudioMixer AudioMixer;//������������Ч�Ļ����
    [SerializeField] Slider SliderEffect;//��Ч������������Slider

    [Header("ģʽ")]
    [SerializeField] Toggle DayToggle;//����ģʽ
    [SerializeField] Toggle NightToggle;//ҹ��ģʽ
    [SerializeField] Light DayLight;//�չ�

    public static PauseCav Instance;//����ģʽ
    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    //����������������
    public void BGVolume()
    {
        //BGAudio.volume = SliderBg.value;
        AudioMixer.SetFloat("BGMVolume", SliderBg.value);
    }
    //��Ч��Ч��������
    public void EffectVolume()
    {
        AudioMixer.SetFloat("EffectVolume", SliderEffect.value);
    }

    public void ExitPause()
    {
        Time.timeScale = 1;
        //isPause = false;
        gameObject.SetActive(false);
    }

    //ѡ�����ģʽ
    public void OnDayToggleClike()
    {
        DayLight.intensity = 1f;
    }
    //ѡ��ҹ��ģʽ
    public void OnNightToggleClick()
    {
        DayLight.intensity = 0f;
    }
}
