using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAttack_My : MonoBehaviour
{
    [Header("Weapon Specs")]
    public float Cooldown = 1f;//������ȴʱ��

    [SerializeField] int damage = 50;//�Ե��˵��˺�ֵ
    [SerializeField] float range = 20.0f;//���߳��ȣ���������Χ��
    [SerializeField] LayerMask strikeableMask;//���յ������Ĳ㼶

    [Header("Waapon References")]
    [SerializeField] LightningBolt_My lightningBolt;
    [SerializeField] AVPlayer_My lightnightHit;


    public void Fire()
    {
        //��������
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;
        if (Physics.Raycast(ray,out hit,range,strikeableMask))
        {

            //���繥���Ľ�����
            lightnightHit.transform.position = hit.point;
            //������ʾ����ķ���
            lightnightHit.Play();

            lightningBolt.EndPoint = hit.point;

            EnemyHealth_My enemyHealth = hit.collider.GetComponent<EnemyHealth_My>();

            //���������û���͵����յ��˺�
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
