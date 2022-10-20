using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth_My : MonoBehaviour
{
    [Header("Health Properties")]
    [SerializeField] int maxHealth = 100;//定义玩家生命值
    [SerializeField] AudioClip deathClip = null;//音乐

    [Header("Script References")]
    [SerializeField] PlayerMovement_My playerMovement;
    //TO DO 玩家射击脚本

    [Header("Components")]
    [SerializeField] Animator animator;
    [SerializeField] AudioSource audioSource;

    //TO DO UI

    [Header("Debugging Properties")]
    [SerializeField] bool isInvulnerable = false;

    int currentHealth;

    //安全代码：给变量赋值
    private void Reset()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement_My>();
        //TO DO 射击
    }

    // Start is called before the first frame update
    void Awake()
    {
        //开始时当前血量等于，血量最大值
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        //不活着直接返回
        if(!IsAlive())
        {
            return;
        }
        //如果不是处于免疫状态
        if(!isInvulnerable)
        {
            //扣除玩家的血量
            currentHealth -= amount;
            Debug.Log("玩家血量：" + currentHealth);
        }
        //TO DO 显示UI

        if(!IsAlive())
        {
            //没懂
            if(playerMovement!=null)
            {
                playerMovement.Defeated();
            }
            //TO DO发射相关代码

            //播放死亡动画
            animator.SetTrigger("Die");
            if(audioSource!=null)
            {
                //播放死亡音效
                audioSource.clip = deathClip;
            }

            //TO DO调用死亡代码
        }
    }
    //判断角色是否活着
    public bool IsAlive()
    {
        return currentHealth > 0;
    }
    //判断当前死亡的玩家是否是自己
    void DeathComplete()
    {
        if(GameManager_My.Instance.Player==this)
        {
            GameManager_My.Instance.PlayerDeathComplete();
        }

    }
}
