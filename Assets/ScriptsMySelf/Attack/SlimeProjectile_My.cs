//这个脚本处理粘液弹。黏液弹是一种寻的攻击，这意味着它不能错过。

//一旦性腺到达目标敌人，它会给敌人附加一个黏液脱附物
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeProjectile_My : MonoBehaviour
{
    [Header("Projectile Properties")]
    [SerializeField] float speed = 20f;//速度
    [SerializeField] float projectileRadiud = 1f;//粘液范围

    [Header("Projectile Reference")]
    [SerializeField] SlimeDebuff_My slimeDebuff;
    [SerializeField] AVPlayer_My slimeHit;

    Transform attackTarget;//攻击的目标位置
    bool isFlying;//是否处于飞行状态

    private void OnEnable()
    {
        isFlying = false;
    }

    public void StartPath(Transform target)
    {
        attackTarget = target;
        isFlying = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(!isFlying)
        {
            return;
        }
        if(attackTarget==null)
        {
            gameObject.SetActive(false);
            
        }
        transform.LookAt(attackTarget);

        transform.Translate(0f, 0f, speed * Time.deltaTime);

        //范围内爆炸
        if(Vector3.Distance(transform.position,attackTarget.position)<=projectileRadiud)
        {
            //爆炸
            Explode();
        }
    }
    //爆炸方法
    void Explode()
    {
        isFlying = false;

        slimeHit.transform.position = attackTarget.position;

        slimeHit.Play();

        EnemyAttack_My enemy = attackTarget.GetComponent<EnemyAttack_My>();

        if(enemy!=null)
        {
            slimeDebuff.gameObject.SetActive(true);

            slimeDebuff.AttachToEnemy(enemy);
        }

        gameObject.SetActive(false);
    }
}
