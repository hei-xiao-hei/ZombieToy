using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack_My : MonoBehaviour
{
    //各种攻击脚本
    [Header("Attacks")]
    [SerializeField] LightningAttack_My lightningAttack;//闪电攻击
    [SerializeField] FrostAttack_My frostAttack;//霜冻攻击
    [SerializeField] StinkAttack_My stinkAttack;//臭味弹攻击
    [SerializeField] SlimeAttack_My slimeAttack;//粘液攻击
    [SerializeField] int numberOfAttacks;//攻击种类的数量

    //滑动条的控制
    [Header("UI")]
    [SerializeField] Countdown_My countdown;

    int attackIndex = 0;//攻击的索引，即用于表示现在是什么攻击
    float attackCooldown = 0f;//攻击的冷却时间
    float timeOfLastAttack = 0f;//攻击间隔时间
    public bool canAttack = false;//是否能攻击

    //切换攻击方式
    public void SwitchAttack()
    {
        //如果当前处于不能攻击的状态则直接退出该方法。
        if(!canAttack)
        {
            return;
        }

        //切换另一种种攻击方式。
        attackIndex++;
        //如果当前攻击的所以大于攻击的总数，则直接将攻击的索引初始化，防止下标越界
        if(attackIndex>=numberOfAttacks)
        {
            attackIndex = 0;
        }

        //关闭所有攻击
        DisableAttacks();

        //根据攻击的索引，调用不同类型的攻击
        switch(attackIndex)
        {
            case 0:
                if(lightningAttack!=null)
                {
                    lightningAttack.gameObject.SetActive(true);
                }
                break;
            case 1:
                if(frostAttack!=null)
                {
                    frostAttack.gameObject.SetActive(true);
                }
                break;
            case 2:
                if(stinkAttack!=null)
                {
                    stinkAttack.gameObject.SetActive(true);
                }
                break;
            case 3:
                if(slimeAttack!=null)
                {
                    slimeAttack.gameObject.SetActive(true);
                }
                break;
        }
    }

    //开火函数
    public void Fire()
    {
        //如果还没准备好攻击或当前不能攻击
        if(!ReadyToAttack()||!canAttack)
        {
            return;
        }
        switch(attackIndex)
        {
            
            case 0:
                //闪电攻击
                ShootLightning();
                break;
            case 1:
                //霜冻攻击
                ToggleFrost(true);
                break;

        }
        
    }
    //停止开火函数
    public void StopFiring()
    {
        //如果还没准备好攻击或当前不能攻击
        if (!ReadyToAttack() || !canAttack)
        {
            return;
        }

        switch (attackIndex)
        {
            case 1:
                //关闭霜冻攻击
                ToggleFrost(false);
                break;
            case 2:
                //控制臭味弹
                ShootStink();
                break;
            case 3:
                //控制粘液攻击
                ShootSlime();
                break;

        }
    }
    //闪电攻击
    void ShootLightning()
    {
        if(lightningAttack==null)
        {
            return;
        }
        lightningAttack.Fire();//调用闪电攻击方法
        attackCooldown = lightningAttack.Cooldown;//闪电攻击的冷却时间

        //开始倒计时，可以下次攻击的时间
        BeginCountdown();
    }
    //冻霜攻击

    void ToggleFrost(bool isAttacking)
    {
        if(frostAttack==null)
        {
            return;
        }
        if(isAttacking)
        {
            frostAttack.Fire();//调用冰霜攻击方法
        }
        else
        {
            frostAttack.StopFiring();//停止冰霜攻击
        }
    }
    //臭味弹攻击
    void ShootStink()
    {
        if(stinkAttack==null)
        {
            return;
        }
        stinkAttack.Fire();//臭味弹攻击
        attackCooldown = stinkAttack.Cooldown;//臭味弹攻击的冷却时间
        //开始倒计时，可以下次攻击的时间
        BeginCountdown();
    }
    //粘液攻击
    void ShootSlime()
    {
        if(slimeAttack==null)
        {
            return;
        }
        if(slimeAttack.Fire())
        {
            attackCooldown = slimeAttack.Cooldown;//粘液攻击的冷却时间

            //开始倒计时，可以下次攻击的时间
            BeginCountdown();
        }
    }

    //是否准备好攻击
    bool ReadyToAttack()
    {
        return Time.time >= timeOfLastAttack + attackCooldown;
    }

    public void Defeated()
    {
        //将canAttack设置为false，表示不能攻击
        canAttack = false;
        //停止攻击
        DisableAttacks();
    }

    ////此方法设置倒数计时，直到玩家可以再次攻击
    void BeginCountdown()
    {
        timeOfLastAttack = Time.time;
        if(countdown!=null)
        {
            countdown.BeginCountDown(attackCooldown);
        }
    }
    
    //关闭所有攻击
    void DisableAttacks()
    {
        if(lightningAttack!=null)
        {
            lightningAttack.gameObject.SetActive(false);
        }
        if(frostAttack!=null)
        {
            frostAttack.gameObject.SetActive(false);
        }
        if(stinkAttack!=null)
        {
            stinkAttack.gameObject.SetActive(false);
        }
        if(slimeAttack!=null)
        {
            slimeAttack.gameObject.SetActive(false);
        }
    }
}
