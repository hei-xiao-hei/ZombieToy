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

    //脚本激活的时候
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
        //如果导航网格激活的话，进入循环
        while(navMeshAgent.enabled)
        {
            Transform target = GameManager_My.Instance.EnemyTarget;//更新敌人的目标

            //不太懂个这个代码
            if(isRunningAway)
            {
                navMeshAgent.SetDestination(runAwayPosition);//设置敌人的目的地
            }
            //如果目标不为空的话
            else if(target != null)
            {
                navMeshAgent.SetDestination(target.position);//敌人导航的目的地就是敌人的目标位置。
            }
            yield return updateDelay;
        }
    }

    //敌人死亡之后
    public void Defeated()
    {
        //网格导航关闭
        navMeshAgent.enabled = false;
        //现实受到攻击的粒子
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
