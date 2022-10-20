//此脚本处理霜冻攻击。霜冻攻击是一种持续的攻击，对敌人产生霜冻效果

//在它的锥形效应中。攻击没有冷却时间，使用弧形网格碰撞器来确定

//敌人在射程内
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostAttack_My : MonoBehaviour
{
    [Header("Weapon Specs")]
    [SerializeField] int maxFreezableEnemies = 20;//一次最多可冰冻敌人数量

    [Header("Weapon References")]
    [SerializeField] GameObject frostDebuffPrefab;
    [SerializeField] GameObject frostCone;
    [SerializeField] MeshCollider frostArc;

    FrostDebuff_My[] debuffs;
    bool isFiring = false;

    private void Reset()
    {
        frostCone = transform.GetChild(1).gameObject;
        frostArc = GetComponentInChildren<MeshCollider>();
    }

    private void Awake()
    {
        debuffs = new FrostDebuff_My[maxFreezableEnemies];

        for(int i=0;i<maxFreezableEnemies;i++)
        {
            GameObject obj = (GameObject)Instantiate(frostDebuffPrefab);

            obj.SetActive(false);

            debuffs[i] = obj.GetComponent<FrostDebuff_My>();
        }
    }

    private void OnDisable()
    {
        //停止开火
        StopFiring();
    }
    //开火函数
    public void Fire()
    {
        if(!isFiring)
        {
            frostCone.SetActive(true);
            frostArc.enabled = true;

            isFiring = true;
        }
    }

    //停止开火
    public void StopFiring()
    {
        if(!isFiring)
        {
            return;
        }

        frostCone.SetActive(false);
        frostArc.enabled = false;

        isFiring = false;

        for(int i=0;i<debuffs.Length;i++)
        {
            if(debuffs[i].gameObject.activeInHierarchy)
            {
                debuffs[i].ReleaseEnemy();
            }
        }
    }

    void AttachDebuffToEnemy(EnemyMovement_My enemy)
    {
        for(int i=0;i<debuffs.Length;i++)
        {
            debuffs[i].gameObject.SetActive(true);
            debuffs[i].AttachToEnemy(enemy);

            return;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        EnemyMovement_My enemy = other.GetComponent<EnemyMovement_My>();

        if(enemy==null)
        {
            return;
        }

        if(enemy.FrostDebuff!=null)
        {
            enemy.FrostDebuff.AttachToEnemy(enemy);
        }
        else
        {
            AttachDebuffToEnemy(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EnemyMovement_My enemy = other.GetComponent<EnemyMovement_My>();
        if(enemy==null)
        {
            return;
        }
        if(enemy.FrostDebuff!=null)
        {
            enemy.FrostDebuff.ReleaseEnemy();
        }
    }
}
