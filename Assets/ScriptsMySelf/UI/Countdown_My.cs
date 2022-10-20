using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown_My : MonoBehaviour
{
    [SerializeField] Slider slider;//滑动条UI

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

    //在进行冷却攻击时，由玩家攻击脚本调用
    public void BeginCountDown(float cooldown)
    {
        timeOfCountdownFinish = Time.time + cooldown;

        slider.maxValue = cooldown;
        slider.value = cooldown;

        //TODO控制滑动条的值

    }
    //这个协同程序减少了滑块的值，这样玩家可以看到他们的攻击何时准备好
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
