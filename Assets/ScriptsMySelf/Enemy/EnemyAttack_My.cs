using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack_My : MonoBehaviour
{
    [HideInInspector] public SlimeDebuff_My SlimeDebuff;

    [SerializeField] float timeBetweenAttacks = 0.5f;//敌人攻击间隔时间
    [SerializeField] int attackDamage = 10;//敌人的攻击值
    [SerializeField] Animator animator;

    bool canAttack;//是否处于可以攻击的状态
    bool playerInRange;//玩家是否在范围内

    WaitForSeconds attackDelay;//攻击间隔时间


    private void Reset()
    {
        animator = GetComponent<Animator>();
    }
    private void Awake()
    {
        attackDelay = new WaitForSeconds(timeBetweenAttacks);
    }

    //脚本激活时，调用
    private void OnEnable()
    {
        SlimeDebuff = null;
        canAttack = true;//当脚本激活时，可以攻击

        //调用协程模式攻击玩家
        StartCoroutine("AttackPlayer");
    }

    //开始触发后调用该方法
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("敌人触碰到：" + other.gameObject.name);
        //如果与敌人发生触发检测的是玩家
        if(other.transform==GameManager_My.Instance.Player.transform)
        {
            //玩家在范围内
            playerInRange = true;
        }
    }

    //结束触发后调用该方法
    private void OnTriggerExit(Collider other)
    {
        //玩家与敌人触发结束后
        if(other.transform==GameManager_My.Instance.Player.transform)
        {
            //玩家不在范围内
            playerInRange = false;
        }
    }

    IEnumerator AttackPlayer()
    {
        Debug.Log("qian");
        yield return null;
        Debug.Log("hou");

        if(GameManager_My.Instance==null)
        {
            yield break;
        }
        //当敌人处于可攻击模式且玩家还活着
        while(canAttack&&CheckPlayerStatus())
        {
            //如果玩家在范围内且SlimeDebuff为空
            if(playerInRange&&SlimeDebuff==null)
            {
                //玩家收到伤害
                GameManager.Instance.Player.TakeDamage(attackDamage);
            }
            yield return attackDelay;
        }
    }
    bool CheckPlayerStatus()
    {
        //如果玩家还活着，返回true
        if(GameManager_My.Instance.Player.IsAlive())
        {
            return true;
        }
        //否则，玩家死亡
        animator.SetTrigger("PlayerDead");//播放玩家死亡动画

        //调用死亡函数
        Defeated();

        return false;
    }

    //敌人死亡方法
    public void Defeated()
    {
        canAttack = false;//敌人死亡后，不能攻击
        if(SlimeDebuff!=null)
        {
            SlimeDebuff.ReleaseEnemy();
        }
    }


}
