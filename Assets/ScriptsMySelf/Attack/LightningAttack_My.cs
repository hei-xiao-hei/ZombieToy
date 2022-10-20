using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAttack_My : MonoBehaviour
{
    [Header("Weapon Specs")]
    public float Cooldown = 1f;//技能冷却时间

    [SerializeField] int damage = 50;//对敌人的伤害值
    [SerializeField] float range = 20.0f;//射线长度（即攻击范围）
    [SerializeField] LayerMask strikeableMask;//能收到攻击的层级

    [Header("Waapon References")]
    [SerializeField] LightningBolt_My lightningBolt;
    [SerializeField] AVPlayer_My lightnightHit;


    public void Fire()
    {
        //生成射线
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;
        if (Physics.Raycast(ray,out hit,range,strikeableMask))
        {

            //闪电攻击的结束点
            lightnightHit.transform.position = hit.point;
            //调用显示闪电的方法
            lightnightHit.Play();

            lightningBolt.EndPoint = hit.point;

            EnemyHealth_My enemyHealth = hit.collider.GetComponent<EnemyHealth_My>();

            //敌人如果还没死就敌人收到伤害
            if(enemyHealth!=null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
        else
        {
            lightningBolt.EndPoint = ray.GetPoint(range);
        }
        lightningBolt.gameObject.SetActive(true);
    }
}
