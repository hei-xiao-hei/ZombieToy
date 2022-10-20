using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement_My : MonoBehaviour
{
    [HideInInspector] public FrostDebuff_My FrostDebuff;
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] Animator animator;

    [Header("Components")]
    [SerializeField] float runAwayDistance = 10f;

    float originalSpeed;
    bool isRunningAway;
    Vector3 runAwayPosition;

    static WaitForSeconds updateDelay = new WaitForSeconds(0.5f);


    private void Reset()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    //�ű������ʱ��
    private void OnEnable()
    {
        navMeshAgent.enabled = true;
        isRunningAway = false;
        StartCoroutine("ChasePlayer");
    }

    IEnumerator ChasePlayer()
    {
        yield return null;

        if(GameManager_My.Instance==null)
        {
            yield break;
        }
        //����������񼤻�Ļ�������ѭ��
        while(navMeshAgent.enabled)
        {
            Transform target = GameManager_My.Instance.EnemyTarget;//���µ��˵�Ŀ��

            //��̫�����������
            if(isRunningAway)
            {
                navMeshAgent.SetDestination(runAwayPosition);//���õ��˵�Ŀ�ĵ�
            }
            //���Ŀ�겻Ϊ�յĻ�
            else if(target != null)
            {
                navMeshAgent.SetDestination(target.position);//���˵�����Ŀ�ĵؾ��ǵ��˵�Ŀ��λ�á�
            }
            yield return updateDelay;
        }
    }

    //��������֮��
    public void Defeated()
    {
        //���񵼺��ر�
        navMeshAgent.enabled = false;
        //��ʵ�ܵ�����������
        if(FrostDebuff != null)
        {
            FrostDebuff.gameObject.SetActive(false);
        }
    }

    public void Freeze()
    {
        animator.enabled = false;
        originalSpeed = navMeshAgent.speed;
        navMeshAgent.speed = 0f;
    }
    public void UnFreeze()
    {
        animator.enabled = true;
        navMeshAgent.speed = originalSpeed;
    }

    public void Runaway()
    {
        isRunningAway = true;
        Vector3 runVector = transform.localPosition - GameManager_My.Instance.EnemyTarget.position;
        runAwayPosition = runVector.normalized * runAwayDistance;
    }
    public void ComeBack()
    {
        isRunningAway = false;
    }
}
