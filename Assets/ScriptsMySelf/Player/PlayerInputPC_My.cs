using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputPC_My : MonoBehaviour
{
    //自定义变量（属性）
    [SerializeField] public PlayerMovement_My playerMove_My=null;
    [SerializeField] PlayerAttack_My playerAttack = null;
    //TODO UI界面  

    //安全检测：给变量赋值
    private void Reset()
    {
        playerMove_My = GetComponent<PlayerMovement_My>();
        playerAttack = GetComponent<PlayerAttack_My>();
        //根据类型找到PauseMenu
        //pauseMenu = FindObjectOfType<PauseMenu>();
    }
    
    //条件编译语句
#if UNITY_ANDROID || UNTI_IOS || UNITY_WP8
    void Awake()
    {
        Destroy(this);
    }
#endif
    // Update is called once per frame
    void Update()
    {
        //暂停菜单的设置：如果有暂停菜单UI并且玩家按下取消按钮，则暂停游戏
       /* if(pauseMenu!=null&Input.GetButtonDown("Cancel"))
        {
            pauseMenu.Pause();
        }*/
        if(!CanUpdate())
        {
            return;
        }
        HandleMoveInput();
        HandleAttackInput();
        HandleAllyInput();
    }
    
    //判断游戏是否暂停的函数
    bool CanUpdate()
    {
        /*//如果暂停UI不为空，且在暂停状态，则返回false
        if(pauseMenu!=null&&pauseMenu.IsPaused)
        {
            return false;
        }*/

        /*if(GameManager.Instance.Player==null||GameManager.Instance.transform!=transform)
        {
            return false;
        }*/
        
        return true;
    }
    void HandleMoveInput()
    {
        //安全判断，如果输入为空，跳出函数
        if (playerMove_My == null)
        {
            return;
        }
        //获取水平和垂直虚拟轴的值，并设置玩家移动的位置
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        //Debug.Log("输入：" + horizontal);

        playerMove_My.MoveDirection = new Vector3(horizontal, 0, vertical);

        //如果鼠标位置不为空且鼠标的位置可以访问
        if(MouseLocation_My.Instance!=null&& MouseLocation_My.Instance.IsValid)
        {
            
            Vector3 lookPoint = MouseLocation_My.Instance.MousePosition - playerMove_My.transform.position;

            playerMove_My.LookDirection = lookPoint;
        }
    }
    
    //玩家射击函数
    void HandleAttackInput()
    {
        //当前不能射击，直接返回
        if(playerAttack==null)
        {
            return;
        }
        if(Input.GetButtonDown("SwitchAttack"))
        {
            playerAttack.SwitchAttack();
        }
        //按下发射按钮
        if(Input.GetButton("Fire1"))
        {
            playerAttack.Fire();
        }
        //松开发射按键，停止发射炮弹
        else if(Input.GetButtonUp("Fire1"))
        {
            playerAttack.StopFiring();
        }

    }

    //没看懂这个函数干啥的
    void HandleAllyInput()
    {
        if(Input.GetButtonDown("SummonAlly")&&GameManager.Instance!=null)
        {
            GameManager.Instance.SummonAlly();
        }
    }
}
