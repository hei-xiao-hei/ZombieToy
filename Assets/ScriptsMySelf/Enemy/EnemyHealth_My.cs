using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth_My : MonoBehaviour
{
    [HideInInspector]public EnemySpawner_My Spawner;

    [Header("Health Proerties")]
    [SerializeField] int maxHealth = 100;//敌人的生命值
    [SerializeField] int scoreValue = 10;//敌人的分数值

    [Header("Defeated Effects")]
    [SerializeField] float sinkSpeed = 0.5f;//敌人移动速度 
    [SerializeField] float deathEffectTime = 2f;//敌人死亡延时小时的时间
    [SerializeField] AudioClip deathClip = null;//死亡音效
    [SerializeField] AudioClip hurtClip = null;//受伤音效

    [Header("Script References")]
    [SerializeField] EnemyAttack_My enemyAttack;
    [SerializeField] EnemyMovement_My enemyMovement;

    [Header("Components")]
    [SerializeField] Animator animator;
    [SerializeField] AudioSource audioSource;
    [SerializeField] CapsuleCollider capsuleCollider;
    [SerializeField] ParticleSystem hitParticles;//ParticleSystem是Unity专门用于制作特效的系统

    [Header("Debugging Properties")]
    [SerializeField] bool isInvulnerable;//敌人是否处于免疫状态

    int currentHealth;//敌人当前血量
    bool isSinking;//敌人是否死亡

    private void Reset()
    {
        enemyAttack = GetComponent<EnemyAttack_My>();
        enemyMovement = GetComponent<EnemyMovement_My>();

        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        hitParticles = GetComponent<ParticleSystem>();
    }

    //当脚本被激活的时候
    private void OnEnable()
    {
        currentHealth = maxHealth;//敌人当前的血量赋初始值
        isSinking = false;
        capsuleCollider.isTrigger = false;//设置为碰撞体

        if(audioSource!=null)
        {
            audioSource.clip = hurtClip;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(!isSinking)
        {
            return;
        }
        //敌人位移
        transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
    }

    //敌人受伤时调用此脚本
    public void TakeDamage(int amount)
    {
        //如果敌人当前血量小于等于0，或者处于免疫状态直接return
        if(currentHealth<=0||isInvulnerable)
        {
            return;
        }
        //敌人减血量
        currentHealth -= amount;

        //如果当前血量小于等于0，则调用死亡脚本
        if(currentHealth<=0)
        {
            Defeated();
        }
        //播放敌人受伤音效
        if(audioSource!=null)
        {
            audioSource.Play();
        }
        //播放敌人受伤粒子。
        hitParticles.Play();
    }

    //敌人死亡方法
    void Defeated()
    {
        capsuleCollider.isTrigger = true;//将敌人设置有触发体
        enemyMovement.Defeated();//调用玩家移动的死亡方法（关闭导航）
        animator.enabled = true;//开启动画状态机
        animator.SetTrigger("Dead");//播放死亡动画

        if(audioSource!=null)
        {
            audioSource.clip = deathClip;
        }

        //TO DO敌人攻击和移动死亡后的方法

        GameManager_My.Instance.AddScore(scoreValue);//添加玩家分数

        Invoke("TurnOff", deathEffectTime);//延时几秒后，调用TurnOff函数
    }

    //玩家消失方法
    void TurnOff()
    {
        gameObject.SetActive(false);
    }

    //没理解这方法干啥的
    public void StartSinking()
    {
        isSinking = true;
    }
}
