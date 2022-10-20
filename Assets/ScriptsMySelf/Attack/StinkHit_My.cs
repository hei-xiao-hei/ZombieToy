//����ű������ζ���еġ��ɽ����������ɽ�������һ�������˼�ǹ�������һ���뾶�ڵ����е��ˣ��򡰽��䡱�������򣩡�

//�ɽ�ײ��������ͼ��Ч���������¸������ڵĵ��˴����������ߡ�
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StinkHit_My : MonoBehaviour
{
    [SerializeField] float explosionRadius = 3f;//�ɽ������ķ�Χ
    [SerializeField] float explosionDuration = 4f;//�ɽ���������ʱ��
    [SerializeField] LayerMask whatIsShootable;//�㼶

    Collider[] enemiesHit;//���˵���ײ��

    private void OnEnable()
    {
        enemiesHit = Physics.OverlapSphere(transform.position, explosionRadius, whatIsShootable);

        for(int i=0;i<enemiesHit.Length;i++)
        {
            EnemyMovement_My enemyMovement = enemiesHit[i].GetComponent<EnemyMovement_My>();

            //�����������
            if(enemyMovement!=null)
            {
                enemyMovement.Runaway();
            }
        }

        //����ֹͣ�ƶ�
        Invoke("StopExploding", explosionDuration);

    }
    void StopExploding()
    {
        for(int i=0;i<enemiesHit.Length;i++)
        {
            EnemyMovement_My enemyMovement = enemiesHit[i].GetComponent<EnemyMovement_My>();

            //����ֹͣ�ƶ�
            if(enemyMovement!=null)
            {
                enemyMovement.ComeBack();
            }
        }
        gameObject.SetActive(false);
    }
}
