using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseCav : MonoBehaviour
{
    [Header("背景音乐")]
    [SerializeField] Slider SliderBg;//背景音乐音量调节Slider
    [SerializeField] AudioMixer AudioMixer;//背景音乐与音效的混合器
    [SerializeField] Slider SliderEffect;//特效音乐音量调节Slider

    [Header("模式")]
    [SerializeField] Toggle DayToggle;//白天模式
    [SerializeField] Toggle NightToggle;//夜晚模式
    [SerializeField] Light DayLight;//日光

    public static PauseCav Instance;//单例模式
    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    //背景音乐音量调节
    public void BGVolume()
    {
        //BGAudio.volume = SliderBg.value;
        AudioMixer.SetFloat("BGMVolume", SliderBg.value);
    }
    //特效音效音量调节
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

    //选择白天模式
    public void OnDayToggleClike()
    {
        DayLight.intensity = 1f;
    }
    //选择夜晚模式
    public void OnNightToggleClick()
    {
        DayLight.intensity = 0f;
    }
}
