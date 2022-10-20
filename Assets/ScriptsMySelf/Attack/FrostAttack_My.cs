//�˽ű�����˪��������˪��������һ�ֳ����Ĺ������Ե��˲���˪��Ч��

//������׶��ЧӦ�С�����û����ȴʱ�䣬ʹ�û���������ײ����ȷ��

//�����������
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostAttack_My : MonoBehaviour
{
    [Header("Weapon Specs")]
    [SerializeField] int maxFreezableEnemies = 20;//һ�����ɱ�����������

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
        //ֹͣ����
        StopFiring();
    }
    //������
    public void Fire()
    {
        if(!isFiring)
        {
            frostCone.SetActive(true);
            frostArc.enabled = true;

            isFiring = true;
        }
    }

    //ֹͣ����
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
