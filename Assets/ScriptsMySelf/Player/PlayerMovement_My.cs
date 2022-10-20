using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_My : MonoBehaviour
{
    public static PlayerMovement_My Instance;

    [HideInInspector] public Vector3 MoveDirection = Vector3.zero;//默认移动位置为
    [HideInInspector] public Vector3 LookDirection = Vector3.forward;//玩家默认看向前方

    [SerializeField] float speed = 6.0f;//移动速度
    [SerializeField] Animator animator;//动画
    [SerializeField] Rigidbody rigidBody;//刚体

    public bool canMove = false;//是否是可移动状态
    // Start is called before the first frame update

    //安全代码
    void Reset()
    {
        //组件赋值给变量
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();

        
    }
    void Awake()
    {
        Instance = this;
    }

    void FixedUpdate()
    {

        //如果不能移动，直接返回
        if(!canMove)
        {
            return;
        }
        //将移动参数的y轴设置为0
        MoveDirection.Set(MoveDirection.x, 0, MoveDirection.z);

        //这句代码还不是里面的参数还不是很懂
        rigidBody.MovePosition(transform.position + MoveDirection.normalized * speed * Time.deltaTime);

        //将玩家看向的位置的y轴设置为0
        LookDirection.Set(LookDirection.x, 0, LookDirection.z);
        //玩家看向目标位置（还不是很懂函数里的参数）
        rigidBody.MoveRotation(Quaternion.LookRotation(LookDirection));
        

        //播放动画(sqrMagnitude是啥忘记了）
        animator.SetBool("IsWalking", MoveDirection.sqrMagnitude > 0);



    }

    //玩家不能在移动时调用该方法
    public void Defeated()
    {
        canMove = false;
    }
}
