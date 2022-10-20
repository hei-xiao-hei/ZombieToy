using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLocation_My : MonoBehaviour
{
    public static MouseLocation_My Instance;//单例模式

    [HideInInspector] public Vector3 MousePosition;//鼠标位置
    [HideInInspector] public bool IsValid;//用于判断当前鼠标是否有效

    //暂时不知道这个干啥的
    [SerializeField] LayerMask whatIsGround;//

    Ray mouseRay;//鼠标射线
    RaycastHit hit;//用于存储鼠标射线信息
    Vector2 screenPosition;//鼠标平面上的坐标
    //不知道干啥的
    bool isTouchAiming;

    private void Awake()
    {
        //单例模式赋值
        if(Instance==null)
        {
            Instance = this;
        }
        //不太理解为什么单例赋值的不是自己就要删掉自己。
        else if(Instance!=this)
        {
            Destroy(this);
        }
    }
    // Update is called once per frame
    void Update()
    {
        IsValid = false;//开始时鼠标无效
#if UNITY_ANDROID || UNITY_IOS || UNITY_WP8
        if(!isTouchAiming)
        {
            return;
        }
#else
        screenPosition = Input.mousePosition;
#endif
        //生成一条从鼠标在屏幕所在的位置发射的射线
        mouseRay = Camera.main.ScreenPointToRay(screenPosition);

        //射线检测（排除whatIsGround层级的对象）
        if (Physics.Raycast(mouseRay,out hit,100f,whatIsGround))
        {
            IsValid = true;
            MousePosition = hit.point;
        }
    }

    //没懂
    public void StartTouchAim(Vector2 position)
    {
        isTouchAiming = true;
        screenPosition = position;
    }
    
    //跟新每一帧屏幕上鼠标的位置
    public void UpdatePostion(Vector2 position)
    {
        screenPosition = position;
    }

    //没太懂
    public void StopTouchAim()
    {
        isTouchAiming = false;
    }
}
