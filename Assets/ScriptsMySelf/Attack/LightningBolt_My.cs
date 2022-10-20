using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���ڴ������繥���е�����ͼ��
public class LightningBolt_My : MonoBehaviour
{
    [HideInInspector] public Vector3 EndPoint;//����ͼ�εĽ���λ��

    [Header("Bolt Properties")]
    [SerializeField] float rayHeight = 2.0f;//���ߵĸ߶�
    [SerializeField] float effectDuration = 0.75f;//������Ч����ʱ��
    [SerializeField] float phaseDuration = 0.1f;//ͣ����ʱ��

    [Header("Bolt rendering")]
    [SerializeField] LineRenderer rayRenderer;//��Ⱦ����
    [SerializeField] AnimationCurve[] rayPhases;//����

    int phaseIndex = 0;
    float timeToChangePhase;
    float timeSinceEffectStarted;
    Vector3 vectorOfBolt;

    private void OnEnable()
    {
        timeToChangePhase = 0f;
        timeSinceEffectStarted = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        //�������缤���ʱ��
        timeSinceEffectStarted += Time.deltaTime;

        //�������ʱ�����������Ч����ʱ�䣬��ر���Ч
        if(timeSinceEffectStarted>=effectDuration)
        {
            gameObject.SetActive(false);
        }

        //���������λ��
        vectorOfBolt = EndPoint - transform.position;

        //������������
        if(timeSinceEffectStarted>=timeToChangePhase)
        {
            
            timeToChangePhase = timeSinceEffectStarted + phaseDuration;

            //��ʾ����
            ChangePhase();
        }
    }

    //��������Ŀ�����������
    void ChangePhase()
    {
        //�±�����
        phaseIndex++;

        //����±�Խ���˾͹���
        if(phaseIndex>=rayPhases.Length)
        {
            phaseIndex = 0;
        }

        //�滭��������
        AnimationCurve curve = rayPhases[phaseIndex];

        rayRenderer.SetVertexCount(curve.keys.Length);

        for(int index=0;index<curve.keys.Length;++index)
        {
            Keyframe key = curve.keys[index];

            Vector3 point = transform.position + vectorOfBolt * key.time;

            point += Vector3.up * key.value * rayHeight;

            rayRenderer.SetPosition(index, point);
        }
    }
}
