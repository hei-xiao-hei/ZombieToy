//这个脚本处理臭味投射。臭味弹是一种“叶状”弹（叶状是一个术语，意思是攻击不是针对目标，而是以垂直弧线行进

//通常需要一段时间才能到达目的地）。弹丸可以在到达目标之前撞击另一个对撞机而中断。一旦受到影响

//这个炮弹爆炸了
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StinkProjectile_My : MonoBehaviour
{
    [SerializeField] float speed = 20f;//速度
    [SerializeField] AnimationCurve arc;//曲线
    [SerializeField] ParticleSystem trailParticles;//粒子系统
    [SerializeField] StinkHit_My stinkHit;//飞溅攻击

    Vector3 startPoint;//开始位置
    Vector3 endPoint;//结束位置
    bool isFlying;//是否处于飞行状态

    //安全代码，赋初值
    private void Reset()
    {
        trailParticles = GetComponent<ParticleSystem>();
    }

    public void StartPath(Vector3 start,Vector3 end)
    {
        isFlying = true;

        startPoint = start;
        endPoint = end;

        StartCoroutine("FollowPath");

    }
    IEnumerator FollowPath()
    {
        trailParticles.Stop(true);//停止粒子播放

        transform.position = startPoint;

        Vector3 pathVector = endPoint - startPoint;

        float totalDistance = pathVector.magnitude;

        float traveledDistance = 0f;

        trailParticles.Play(true);//播放粒子效果

        while(totalDistance-traveledDistance>0f)
        {
            traveledDistance += speed * Time.deltaTime;

            Vector3 newPosition = startPoint + (pathVector.normalized * traveledDistance);

            float arcHeight = arc.Evaluate(traveledDistance / totalDistance);
            newPosition.y += arcHeight;

            transform.position = newPosition;

            yield return null;
        }

        //调用爆炸方法
        Explode();
    }

    void Explode()
    {
        isFlying = false;
        //当前位置赋值给爆炸效果的位置
        stinkHit.transform.position = transform.position;
        //显示爆炸效果
        stinkHit.gameObject.SetActive(true);
        //显示臭味弹攻击效果
        gameObject.SetActive(true);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(isFlying)
        {
            Explode();
        }
    }
}
