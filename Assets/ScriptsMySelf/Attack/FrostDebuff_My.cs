using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//冰冻敌人的脚本
public class FrostDebuff_My : MonoBehaviour
{
    [SerializeField] GameObject mist;//薄雾游戏物体
    [SerializeField] GameObject iceBlock;//冰块游戏物体
    [SerializeField] float freezeDelay = 0.5f;//冻结延迟时间
    [SerializeField] float freezeDuration = 10f;//冻结持续时间

    EnemyMovement_My target;//冻结的目标是游戏物体
    float timeToToggleEffect;//切换状态的时间，即常态切换成冻结和冻结切换成常态
    bool isFreezing;//是否正在冻结中
    bool isAttached;//是否被附加

    //在GameObject激活后调用
    private void OnEnable()
    {
        mist.SetActive(true);//显示薄雾
        iceBlock.SetActive(false);//显示冰块

        isAttached = false;//不是在冻结中
        isFreezing = false;//不是被附加中
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position;//敌人位置赋值给冻结的位置

        //？？？这里的具体作用还不是很懂
        if(!isAttached&&!isFreezing)
        {
            Debug.Log("name:"+target.FrostDebuff);
            if(target.FrostDebuff!=null)
            {
                target.FrostDebuff = null;
            }

            target = null;
            gameObject.SetActive(false);
        }
        //如果被冻结中且不处于附加状态
        else if(isAttached&&!isFreezing)
        {
            //检查是否被冻结
            CheckForFreeze();
        }
        //如果没被冻结中且不处于附加状态
        else if (!isAttached&&isFreezing)
        {
            //检查是否取消冻结
            CheckForUnFreeze();
        }
    }
    //检查是否冻结
    void CheckForFreeze()
    {
        //如果当前大于切换状态的时间
        if(Time.time>=timeToToggleEffect)
        {
            //冻结敌人
            FreezeTarget();
        }
    }
    //检查是否取消冻结
    void CheckForUnFreeze()
    {
        //如果时间大于切换状态的时间
        if(Time.time>=timeToToggleEffect)
        {
            UnFreezeTarget();
        }
    }
    //冰冻效果附属给敌人
    public void AttachToEnemy(EnemyMovement_My enemy)
    {
        if(target!=null)
        {
            return;
        }
        target = enemy;
        target.FrostDebuff = this;

        isAttached = true;

        timeToToggleEffect = Time.time + freezeDelay;

    }

    //释放敌人（即对敌人解冻）
    public void ReleaseEnemy()
    {
        if(target==null)
        {
            return;
        }
        isAttached = false;
        if(isFreezing)
        {
            timeToToggleEffect = Time.time + freezeDuration;
        }
    }

    //冻结敌人
    void FreezeTarget()
    {
        isFreezing = true;
        target.Freeze();
        mist.SetActive(false);
        iceBlock.SetActive(true);
    }
    //取消冻结
    void UnFreezeTarget()
    {
        isFreezing = false;
        target.UnFreeze();
    }
}
