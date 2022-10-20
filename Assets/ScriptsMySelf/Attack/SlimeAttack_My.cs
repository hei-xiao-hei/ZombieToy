//此脚本处理粘液攻击。黏液攻击是一种目标明确的攻击，需要较长的冷却时间。一个敌人将成为目标

//会被攻击“粘住”。黏液阻止它们攻击，并使它们随着时间受到伤害。仅此攻击

//释放触发器时触发。
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttack_My : MonoBehaviour
{
    [Header("Weapon Specs")]
    public float Cooldown = 3.5f;//技能冷却时间

    [SerializeField] LayerMask whatIsShootable;//受攻击的层级

    [Header("Weapon References")]
    [SerializeField] SlimeProjectile_My slimeProjectile;
    [SerializeField] Renderer targetReticule;

    [Header("Reticule Colors")]
    [SerializeField] Color invalidTargetTint = Color.red;//无效目标的颜色
    [SerializeField] Color notReadyTint = Color.yellow;//攻击没准备好的颜色
    [SerializeField] Color readyTint = Color.green;//攻击准备好的颜色

    float timeOfLastAttack = -10f;//上次攻击的时间，用一个伪值初始化
    Transform target = null;//攻击目标
    Vector3 targetPosition;
    Collider[] testColliders;


    public bool Fire()
    {
        if(target!=null)
        {
            //发射黏液弹
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

        targetPosition = MouseLocation_My.Instance.MousePosition;//鼠标所指的位置为目标位置

        //射线
        RaycastHit hit;
        if(Physics.Raycast(targetPosition,Vector3.up,out hit,2f,whatIsShootable))
        {
            target = hit.transform;
        }
        //更新十字线的位置和颜色
        UpdateReticule();
    }
    //此方法更新十字线的位置和颜色
    void UpdateReticule()
    {
        //更新十字线的位置
        if(target!=null)
        {
            targetReticule.transform.position = target.position;
        }
        else
        {
            targetReticule.transform.position = targetPosition;
        }

        //更新十字线的颜色，三种状态对应三种不同的颜色
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
    //这种方法向目标敌人发射“追击”炮弹
    void LaunchProjectile()
    {
        timeOfLastAttack = Time.time;

        slimeProjectile.transform.position = transform.position;

        slimeProjectile.gameObject.SetActive(true);

        slimeProjectile.StartPath(target);

        target = null;
    }
}
