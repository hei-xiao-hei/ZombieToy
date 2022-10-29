using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputPC_My : MonoBehaviour
{
    //�Զ�����������ԣ�
    [SerializeField] public PlayerMovement_My playerMove_My=null;
    [SerializeField] PlayerAttack_My playerAttack = null;
    //TODO UI����  

    //��ȫ��⣺��������ֵ
    private void Reset()
    {
        playerMove_My = GetComponent<PlayerMovement_My>();
        playerAttack = GetComponent<PlayerAttack_My>();
        //���������ҵ�PauseMenu
        //pauseMenu = FindObjectOfType<PauseMenu>();
    }
    
    //�����������
#if UNITY_ANDROID || UNTI_IOS || UNITY_WP8
    void Awake()
    {
        Destroy(this);
    }
#endif
    // Update is called once per frame
    void Update()
    {
        HandleMoveInput();
        HandleAttackInput();
        HandleAllyInput();
    }
    void HandleMoveInput()
    {
        //��ȫ�жϣ��������Ϊ�գ���������
        if (playerMove_My == null)
        {
            return;
        }
        //��ȡˮƽ�ʹ�ֱ�������ֵ������������ƶ���λ��
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        //Debug.Log("���룺" + horizontal);

        playerMove_My.MoveDirection = new Vector3(horizontal, 0, vertical);

        //������λ�ò�Ϊ��������λ�ÿ��Է���
        if(MouseLocation_My.Instance!=null&& MouseLocation_My.Instance.IsValid)
        {
            
            Vector3 lookPoint = MouseLocation_My.Instance.MousePosition - playerMove_My.transform.position;

            playerMove_My.LookDirection = lookPoint;
        }
    }
    
    //����������
    void HandleAttackInput()
    {
        //��ǰ���������ֱ�ӷ���
        if(playerAttack==null)
        {
            return;
        }
        if(Input.GetButtonDown("SwitchAttack"))
        {
            playerAttack.SwitchAttack();
        }
        //���·��䰴ť
        if(Input.GetButton("Fire1"))
        {
            playerAttack.Fire();
        }
        //�ɿ����䰴����ֹͣ�����ڵ�
        else if(Input.GetButtonUp("Fire1"))
        {
            playerAttack.StopFiring();
        }

    }

    //û�������������ɶ��
    void HandleAllyInput()
    {
        if(Input.GetButtonDown("SummonAlly")&&GameManager.Instance!=null)
        {
            GameManager.Instance.SummonAlly();
        }
    }
}
