//����ű������ζͶ�䡣��ζ����һ�֡�Ҷ״������Ҷ״��һ�������˼�ǹ����������Ŀ�꣬�����Դ�ֱ�����н�

//ͨ����Ҫһ��ʱ����ܵ���Ŀ�ĵأ�����������ڵ���Ŀ��֮ǰײ����һ����ײ�����жϡ�һ���ܵ�Ӱ��

//����ڵ���ը��
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StinkProjectile_My : MonoBehaviour
{
    [SerializeField] float speed = 20f;//�ٶ�
    [SerializeField] AnimationCurve arc;//����
    [SerializeField] ParticleSystem trailParticles;//����ϵͳ
    [SerializeField] StinkHit_My stinkHit;//�ɽ�����

    Vector3 startPoint;//��ʼλ��
    Vector3 endPoint;//����λ��
    bool isFlying;//�Ƿ��ڷ���״̬

    //��ȫ���룬����ֵ
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
        trailParticles.Stop(true);//ֹͣ���Ӳ���

        transform.position = startPoint;

        Vector3 pathVector = endPoint - startPoint;

        float totalDistance = pathVector.magnitude;

        float traveledDistance = 0f;

        trailParticles.Play(true);//��������Ч��

        while(totalDistance-traveledDistance>0f)
        {
            traveledDistance += speed * Time.deltaTime;

            Vector3 newPosition = startPoint + (pathVector.normalized * traveledDistance);

            float arcHeight = arc.Evaluate(traveledDistance / totalDistance);
            newPosition.y += arcHeight;

            transform.position = newPosition;

            yield return null;
        }

        //���ñ�ը����
        Explode();
    }

    void Explode()
    {
        isFlying = false;
        //��ǰλ�ø�ֵ����ըЧ����λ��
        stinkHit.transform.position = transform.position;
        //��ʾ��ըЧ��
        stinkHit.gameObject.SetActive(true);
        //��ʾ��ζ������Ч��
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
