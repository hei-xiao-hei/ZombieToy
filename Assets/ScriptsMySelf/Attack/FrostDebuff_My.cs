using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�������˵Ľű�
public class FrostDebuff_My : MonoBehaviour
{
    [SerializeField] GameObject mist;//������Ϸ����
    [SerializeField] GameObject iceBlock;//������Ϸ����
    [SerializeField] float freezeDelay = 0.5f;//�����ӳ�ʱ��
    [SerializeField] float freezeDuration = 10f;//�������ʱ��

    EnemyMovement_My target;//�����Ŀ������Ϸ����
    float timeToToggleEffect;//�л�״̬��ʱ�䣬����̬�л��ɶ���Ͷ����л��ɳ�̬
    bool isFreezing;//�Ƿ����ڶ�����
    bool isAttached;//�Ƿ񱻸���

    //��GameObject��������
    private void OnEnable()
    {
        mist.SetActive(true);//��ʾ����
        iceBlock.SetActive(false);//��ʾ����

        isAttached = false;//�����ڶ�����
        isFreezing = false;//���Ǳ�������
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position;//����λ�ø�ֵ�������λ��

        //����������ľ������û����Ǻܶ�
        if(!isAttached&&!isFreezing)
        {
            Debug.Log("name:"+target.FrostDebuff);
            if(target.FrostDebuff!=null)
            {
                target.FrostDebuff = null;
            }

            target = null;
            gameObject.SetActive(false);
        }
        //������������Ҳ����ڸ���״̬
        else if(isAttached&&!isFreezing)
        {
            //����Ƿ񱻶���
            CheckForFreeze();
        }
        //���û���������Ҳ����ڸ���״̬
        else if (!isAttached&&isFreezing)
        {
            //����Ƿ�ȡ������
            CheckForUnFreeze();
        }
    }
    //����Ƿ񶳽�
    void CheckForFreeze()
    {
        //�����ǰ�����л�״̬��ʱ��
        if(Time.time>=timeToToggleEffect)
        {
            //�������
            FreezeTarget();
        }
    }
    //����Ƿ�ȡ������
    void CheckForUnFreeze()
    {
        //���ʱ������л�״̬��ʱ��
        if(Time.time>=timeToToggleEffect)
        {
            UnFreezeTarget();
        }
    }
    //����Ч������������
    public void AttachToEnemy(EnemyMovement_My enemy)
    {
        if(target!=null)
        {
            return;
        }
        target = enemy;
        target.FrostDebuff = this;

        isAttached = true;

        timeToToggleEffect = Time.time + freezeDelay;

    }

    //�ͷŵ��ˣ����Ե��˽ⶳ��
    public void ReleaseEnemy()
    {
        if(target==null)
        {
            return;
        }
        isAttached = false;
        if(isFreezing)
        {
            timeToToggleEffect = Time.time + freezeDuration;
        }
    }

    //�������
    void FreezeTarget()
    {
        isFreezing = true;
        target.Freeze();
        mist.SetActive(false);
        iceBlock.SetActive(true);
    }
    //ȡ������
    void UnFreezeTarget()
    {
        isFreezing = false;
        target.UnFreeze();
    }
}
