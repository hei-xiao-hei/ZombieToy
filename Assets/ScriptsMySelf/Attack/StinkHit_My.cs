//这个脚本处理臭味命中的“飞溅”攻击（飞溅攻击是一个术语，意思是攻击击中一个半径内的所有敌人，或“溅落”整个区域）。

//飞溅撞击将发挥图形效果，并导致该区域内的敌人从玩家身边逃走。
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StinkHit_My : MonoBehaviour
{
    [SerializeField] float explosionRadius = 3f;//飞溅攻击的范围
    [SerializeField] float explosionDuration = 4f;//飞溅攻击持续时间
    [SerializeField] LayerMask whatIsShootable;//层级

    Collider[] enemiesHit;//敌人的碰撞体

    private void OnEnable()
    {
        enemiesHit = Physics.OverlapSphere(transform.position, explosionRadius, whatIsShootable);

        for(int i=0;i<enemiesHit.Length;i++)
        {
            EnemyMovement_My enemyMovement = enemiesHit[i].GetComponent<EnemyMovement_My>();

            //敌人走向玩家
            if(enemyMovement!=null)
            {
                enemyMovement.Runaway();
            }
        }

        //敌人停止移动
        Invoke("StopExploding", explosionDuration);

    }
    void StopExploding()
    {
        for(int i=0;i<enemiesHit.Length;i++)
        {
            EnemyMovement_My enemyMovement = enemiesHit[i].GetComponent<EnemyMovement_My>();

            //敌人停止移动
            if(enemyMovement!=null)
            {
                enemyMovement.ComeBack();
            }
        }
        gameObject.SetActive(false);
    }
}
