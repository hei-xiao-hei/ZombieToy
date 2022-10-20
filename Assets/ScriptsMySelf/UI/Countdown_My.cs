using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown_My : MonoBehaviour
{
    [SerializeField] Slider slider;//������UI

    float timeOfCountdownFinish;
    static WaitForSeconds updateDelay = new WaitForSeconds(0.25f);

    private void Reset()
    {
        slider = GetComponent<Slider>();
    }

    private void Awake()
    {
        slider.minValue = 0f;
        slider.maxValue = 0f;
    }

    //�ڽ�����ȴ����ʱ������ҹ����ű�����
    public void BeginCountDown(float cooldown)
    {
        timeOfCountdownFinish = Time.time + cooldown;

        slider.maxValue = cooldown;
        slider.value = cooldown;

        //TODO���ƻ�������ֵ

    }
    //���Эͬ��������˻����ֵ��������ҿ��Կ������ǵĹ�����ʱ׼����
    IEnumerator UpdateCountdownBar()
    {
        while(slider.value>0f)
        {
            slider.value = timeOfCountdownFinish - Time.time;

            yield return updateDelay;
        }
        slider.maxValue = 0f;
    }
}
