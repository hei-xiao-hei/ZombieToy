using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerSelect_My : MonoBehaviour
{
    [Header("Player to Disable")]
    [SerializeField] PlayerSelect_My otherCharacter;//另一个角色
    [SerializeField] PlayerAttack_My PlayerAttack;//玩家攻击脚本

    [Header("References")]
    [SerializeField] CapsuleCollider  capsuleCollider;//碰撞体
    [SerializeField] Rigidbody rigidBody;//刚体
    [SerializeField] Animator animator;//动画
    [SerializeField] PlayerHealth_My playerHealth;//玩家生命值脚本
    //To DO角色生命值和碰撞体


    //安全代码，保证变量都赋值了
    void Reset()
    {
        playerHealth= GetComponent<PlayerHealth_My>();
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        PlayerAttack = GetComponent<PlayerAttack_My>();
    }

    //鼠标抬起选择角色
    private void OnMouseUp()
    {
#if !UNITY_ANDROID&&!UNITY_IOS&&!UNITY_WP8
        //没懂什么意思
        if(EventSystem.current!=null&&EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
#endif
        
        //调用GameManager中的PlayChosen方法
        GameManager_My.Instance.PlayerChosen(playerHealth);

        //另一个对象不为空，则调用删除该对象的方法
        if (otherCharacter != null)
        {
            otherCharacter.DisableSelectableCharater();
        }
        //设置可攻击
        PlayerAttack.canAttack = true;
        enabled = false;
        
    }

    public void DisableSelectableCharater()
    {
        //PlayerAttack.canAttack = false;
        //播放死亡动画
        animator.SetTrigger("Die");
        

    }
    //玩家下沉
    void DeathComplete()
    {
        //另一个对象的碰撞体关闭
        capsuleCollider.enabled = false;
        //一秒后删除对象
        Destroy(gameObject, 3.0f);
    }
}
