//�˽ű�����ճҺ�������Һ������һ��Ŀ����ȷ�Ĺ�������Ҫ�ϳ�����ȴʱ�䡣һ�����˽���ΪĿ��

//�ᱻ������ճס�����Һ��ֹ���ǹ�������ʹ��������ʱ���ܵ��˺������˹���

//�ͷŴ�����ʱ������
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttack_My : MonoBehaviour
{
    [Header("Weapon Specs")]
    public float Cooldown = 3.5f;//������ȴʱ��

    [SerializeField] LayerMask whatIsShootable;//�ܹ����Ĳ㼶

    [Header("Weapon References")]
    [SerializeField] SlimeProjectile_My slimeProjectile;
    [SerializeField] Renderer targetReticule;

    [Header("Reticule Colors")]
    [SerializeField] Color invalidTargetTint = Color.red;//��ЧĿ�����ɫ
    [SerializeField] Color notReadyTint = Color.yellow;//����û׼���õ���ɫ
    [SerializeField] Color readyTint = Color.green;//����׼���õ���ɫ

    float timeOfLastAttack = -10f;//�ϴι�����ʱ�䣬��һ��αֵ��ʼ��
    Transform target = null;//����Ŀ��
    Vector3 targetPosition;
    Collider[] testColliders;


    public bool Fire()
    {
        if(target!=null)
        {
            //�����Һ��
            LaunchProjectile();
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if(MouseLocation_My.Instance==null||!MouseLocation_My.Instance.IsValid)
        {
            return;
        }

        targetPosition = MouseLocation_My.Instance.MousePosition;//�����ָ��λ��ΪĿ��λ��

        //����
        RaycastHit hit;
        if(Physics.Raycast(targetPosition,Vector3.up,out hit,2f,whatIsShootable))
        {
            target = hit.transform;
        }
        //����ʮ���ߵ�λ�ú���ɫ
        UpdateReticule();
    }
    //�˷�������ʮ���ߵ�λ�ú���ɫ
    void UpdateReticule()
    {
        //����ʮ���ߵ�λ��
        if(target!=null)
        {
            targetReticule.transform.position = target.position;
        }
        else
        {
            targetReticule.transform.position = targetPosition;
        }

        //����ʮ���ߵ���ɫ������״̬��Ӧ���ֲ�ͬ����ɫ
        if(target==null)
        {
            targetReticule.material.SetColor("_TintColor", invalidTargetTint);

        }
        else if(timeOfLastAttack+Cooldown>Time.deltaTime)
        {
            targetReticule.material.SetColor("_TintColor", notReadyTint);
        }
        else
        {
            targetReticule.material.SetColor("_TintColor", readyTint); 
        }
    }
    //���ַ�����Ŀ����˷��䡰׷�����ڵ�
    void LaunchProjectile()
    {
        timeOfLastAttack = Time.time;

        slimeProjectile.transform.position = transform.position;

        slimeProjectile.gameObject.SetActive(true);

        slimeProjectile.StartPath(target);

        target = null;
    }
}
