//����ű�����ճҺ�����Һ����һ��Ѱ�Ĺ���������ζ�������ܴ����

//һ�����ٵ���Ŀ����ˣ���������˸���һ���Һ�Ѹ���
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeProjectile_My : MonoBehaviour
{
    [Header("Projectile Properties")]
    [SerializeField] float speed = 20f;//�ٶ�
    [SerializeField] float projectileRadiud = 1f;//ճҺ��Χ

    [Header("Projectile Reference")]
    [SerializeField] SlimeDebuff_My slimeDebuff;
    [SerializeField] AVPlayer_My slimeHit;

    Transform attackTarget;//������Ŀ��λ��
    bool isFlying;//�Ƿ��ڷ���״̬

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

        //��Χ�ڱ�ը
        if(Vector3.Distance(transform.position,attackTarget.position)<=projectileRadiud)
        {
            //��ը
            Explode();
        }
    }
    //��ը����
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
