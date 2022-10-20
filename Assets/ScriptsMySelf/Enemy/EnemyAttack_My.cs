using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack_My : MonoBehaviour
{
    [HideInInspector] public SlimeDebuff_My SlimeDebuff;

    [SerializeField] float timeBetweenAttacks = 0.5f;//���˹������ʱ��
    [SerializeField] int attackDamage = 10;//���˵Ĺ���ֵ
    [SerializeField] Animator animator;

    bool canAttack;//�Ƿ��ڿ��Թ�����״̬
    bool playerInRange;//����Ƿ��ڷ�Χ��

    WaitForSeconds attackDelay;//�������ʱ��


    private void Reset()
    {
        animator = GetComponent<Animator>();
    }
    private void Awake()
    {
        attackDelay = new WaitForSeconds(timeBetweenAttacks);
    }

    //�ű�����ʱ������
    private void OnEnable()
    {
        SlimeDebuff = null;
        canAttack = true;//���ű�����ʱ�����Թ���

        //����Э��ģʽ�������
        StartCoroutine("AttackPlayer");
    }

    //��ʼ��������ø÷���
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("���˴�������" + other.gameObject.name);
        //�������˷����������������
        if(other.transform==GameManager_My.Instance.Player.transform)
        {
            //����ڷ�Χ��
            playerInRange = true;
        }
    }

    //������������ø÷���
    private void OnTriggerExit(Collider other)
    {
        //�������˴���������
        if(other.transform==GameManager_My.Instance.Player.transform)
        {
            //��Ҳ��ڷ�Χ��
            playerInRange = false;
        }
    }

    IEnumerator AttackPlayer()
    {
        Debug.Log("qian");
        yield return null;
        Debug.Log("hou");

        if(GameManager_My.Instance==null)
        {
            yield break;
        }
        //�����˴��ڿɹ���ģʽ����һ�����
        while(canAttack&&CheckPlayerStatus())
        {
            //�������ڷ�Χ����SlimeDebuffΪ��
            if(playerInRange&&SlimeDebuff==null)
            {
                //����յ��˺�
                GameManager.Instance.Player.TakeDamage(attackDamage);
            }
            yield return attackDelay;
        }
    }
    bool CheckPlayerStatus()
    {
        //�����һ����ţ�����true
        if(GameManager_My.Instance.Player.IsAlive())
        {
            return true;
        }
        //�����������
        animator.SetTrigger("PlayerDead");//���������������

        //������������
        Defeated();

        return false;
    }

    //������������
    public void Defeated()
    {
        canAttack = false;//���������󣬲��ܹ���
        if(SlimeDebuff!=null)
        {
            SlimeDebuff.ReleaseEnemy();
        }
    }


}
