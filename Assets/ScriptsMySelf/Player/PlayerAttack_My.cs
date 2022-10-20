using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack_My : MonoBehaviour
{
    //���ֹ����ű�
    [Header("Attacks")]
    [SerializeField] LightningAttack_My lightningAttack;//���繥��
    [SerializeField] FrostAttack_My frostAttack;//˪������
    [SerializeField] StinkAttack_My stinkAttack;//��ζ������
    [SerializeField] SlimeAttack_My slimeAttack;//ճҺ����
    [SerializeField] int numberOfAttacks;//�������������

    //�������Ŀ���
    [Header("UI")]
    [SerializeField] Countdown_My countdown;

    int attackIndex = 0;//�����������������ڱ�ʾ������ʲô����
    float attackCooldown = 0f;//��������ȴʱ��
    float timeOfLastAttack = 0f;//�������ʱ��
    public bool canAttack = false;//�Ƿ��ܹ���

    //�л�������ʽ
    public void SwitchAttack()
    {
        //�����ǰ���ڲ��ܹ�����״̬��ֱ���˳��÷�����
        if(!canAttack)
        {
            return;
        }

        //�л���һ���ֹ�����ʽ��
        attackIndex++;
        //�����ǰ���������Դ��ڹ�������������ֱ�ӽ�������������ʼ������ֹ�±�Խ��
        if(attackIndex>=numberOfAttacks)
        {
            attackIndex = 0;
        }

        //�ر����й���
        DisableAttacks();

        //���ݹ��������������ò�ͬ���͵Ĺ���
        switch(attackIndex)
        {
            case 0:
                if(lightningAttack!=null)
                {
                    lightningAttack.gameObject.SetActive(true);
                }
                break;
            case 1:
                if(frostAttack!=null)
                {
                    frostAttack.gameObject.SetActive(true);
                }
                break;
            case 2:
                if(stinkAttack!=null)
                {
                    stinkAttack.gameObject.SetActive(true);
                }
                break;
            case 3:
                if(slimeAttack!=null)
                {
                    slimeAttack.gameObject.SetActive(true);
                }
                break;
        }
    }

    //������
    public void Fire()
    {
        //�����û׼���ù�����ǰ���ܹ���
        if(!ReadyToAttack()||!canAttack)
        {
            return;
        }
        switch(attackIndex)
        {
            
            case 0:
                //���繥��
                ShootLightning();
                break;
            case 1:
                //˪������
                ToggleFrost(true);
                break;

        }
        
    }
    //ֹͣ������
    public void StopFiring()
    {
        //�����û׼���ù�����ǰ���ܹ���
        if (!ReadyToAttack() || !canAttack)
        {
            return;
        }

        switch (attackIndex)
        {
            case 1:
                //�ر�˪������
                ToggleFrost(false);
                break;
            case 2:
                //���Ƴ�ζ��
                ShootStink();
                break;
            case 3:
                //����ճҺ����
                ShootSlime();
                break;

        }
    }
    //���繥��
    void ShootLightning()
    {
        if(lightningAttack==null)
        {
            return;
        }
        lightningAttack.Fire();//�������繥������
        attackCooldown = lightningAttack.Cooldown;//���繥������ȴʱ��

        //��ʼ����ʱ�������´ι�����ʱ��
        BeginCountdown();
    }
    //��˪����

    void ToggleFrost(bool isAttacking)
    {
        if(frostAttack==null)
        {
            return;
        }
        if(isAttacking)
        {
            frostAttack.Fire();//���ñ�˪��������
        }
        else
        {
            frostAttack.StopFiring();//ֹͣ��˪����
        }
    }
    //��ζ������
    void ShootStink()
    {
        if(stinkAttack==null)
        {
            return;
        }
        stinkAttack.Fire();//��ζ������
        attackCooldown = stinkAttack.Cooldown;//��ζ����������ȴʱ��
        //��ʼ����ʱ�������´ι�����ʱ��
        BeginCountdown();
    }
    //ճҺ����
    void ShootSlime()
    {
        if(slimeAttack==null)
        {
            return;
        }
        if(slimeAttack.Fire())
        {
            attackCooldown = slimeAttack.Cooldown;//ճҺ��������ȴʱ��

            //��ʼ����ʱ�������´ι�����ʱ��
            BeginCountdown();
        }
    }

    //�Ƿ�׼���ù���
    bool ReadyToAttack()
    {
        return Time.time >= timeOfLastAttack + attackCooldown;
    }

    public void Defeated()
    {
        //��canAttack����Ϊfalse����ʾ���ܹ���
        canAttack = false;
        //ֹͣ����
        DisableAttacks();
    }

    ////�˷������õ�����ʱ��ֱ����ҿ����ٴι���
    void BeginCountdown()
    {
        timeOfLastAttack = Time.time;
        if(countdown!=null)
        {
            countdown.BeginCountDown(attackCooldown);
        }
    }
    
    //�ر����й���
    void DisableAttacks()
    {
        if(lightningAttack!=null)
        {
            lightningAttack.gameObject.SetActive(false);
        }
        if(frostAttack!=null)
        {
            frostAttack.gameObject.SetActive(false);
        }
        if(stinkAttack!=null)
        {
            stinkAttack.gameObject.SetActive(false);
        }
        if(slimeAttack!=null)
        {
            slimeAttack.gameObject.SetActive(false);
        }
    }
}
