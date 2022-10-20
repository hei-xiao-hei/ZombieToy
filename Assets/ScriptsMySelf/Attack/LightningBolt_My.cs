using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用于处理闪电攻击中的闪电图形
public class LightningBolt_My : MonoBehaviour
{
    [HideInInspector] public Vector3 EndPoint;//闪电图形的结束位置

    [Header("Bolt Properties")]
    [SerializeField] float rayHeight = 2.0f;//射线的高度
    [SerializeField] float effectDuration = 0.75f;//闪电特效持续时间
    [SerializeField] float phaseDuration = 0.1f;//停留的时间

    [Header("Bolt rendering")]
    [SerializeField] LineRenderer rayRenderer;//渲染线条
    [SerializeField] AnimationCurve[] rayPhases;//曲线

    int phaseIndex = 0;
    float timeToChangePhase;
    float timeSinceEffectStarted;
    Vector3 vectorOfBolt;

    private void OnEnable()
    {
        timeToChangePhase = 0f;
        timeSinceEffectStarted = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        //计算闪电激活的时间
        timeSinceEffectStarted += Time.deltaTime;

        //如果激活时间大于闪电特效保持时间，则关闭特效
        if(timeSinceEffectStarted>=effectDuration)
        {
            gameObject.SetActive(false);
        }

        //闪电的生成位置
        vectorOfBolt = EndPoint - transform.position;

        //如果玩家升级了
        if(timeSinceEffectStarted>=timeToChangePhase)
        {
            
            timeToChangePhase = timeSinceEffectStarted + phaseDuration;

            //显示闪电
            ChangePhase();
        }
    }

    //计算闪电的看似随机的外观
    void ChangePhase()
    {
        //下标增加
        phaseIndex++;

        //如果下标越界了就归零
        if(phaseIndex>=rayPhases.Length)
        {
            phaseIndex = 0;
        }

        //绘画闪电曲线
        AnimationCurve curve = rayPhases[phaseIndex];

        rayRenderer.SetVertexCount(curve.keys.Length);

        for(int index=0;index<curve.keys.Length;++index)
        {
            Keyframe key = curve.keys[index];

            Vector3 point = transform.position + vectorOfBolt * key.time;

            point += Vector3.up * key.value * rayHeight;

            rayRenderer.SetPosition(index, point);
        }
    }
}
