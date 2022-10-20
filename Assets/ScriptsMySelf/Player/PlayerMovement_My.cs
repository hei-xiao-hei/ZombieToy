using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_My : MonoBehaviour
{
    public static PlayerMovement_My Instance;

    [HideInInspector] public Vector3 MoveDirection = Vector3.zero;//Ĭ���ƶ�λ��Ϊ
    [HideInInspector] public Vector3 LookDirection = Vector3.forward;//���Ĭ�Ͽ���ǰ��

    [SerializeField] float speed = 6.0f;//�ƶ��ٶ�
    [SerializeField] Animator animator;//����
    [SerializeField] Rigidbody rigidBody;//����

    public bool canMove = false;//�Ƿ��ǿ��ƶ�״̬
    // Start is called before the first frame update

    //��ȫ����
    void Reset()
    {
        //�����ֵ������
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();

        
    }
    void Awake()
    {
        Instance = this;
    }

    void FixedUpdate()
    {

        //��������ƶ���ֱ�ӷ���
        if(!canMove)
        {
            return;
        }
        //���ƶ�������y������Ϊ0
        MoveDirection.Set(MoveDirection.x, 0, MoveDirection.z);

        //�����뻹��������Ĳ��������Ǻܶ�
        rigidBody.MovePosition(transform.position + MoveDirection.normalized * speed * Time.deltaTime);

        //����ҿ����λ�õ�y������Ϊ0
        LookDirection.Set(LookDirection.x, 0, LookDirection.z);
        //��ҿ���Ŀ��λ�ã������Ǻܶ�������Ĳ�����
        rigidBody.MoveRotation(Quaternion.LookRotation(LookDirection));
        

        //���Ŷ���(sqrMagnitude��ɶ�����ˣ�
        animator.SetBool("IsWalking", MoveDirection.sqrMagnitude > 0);



    }

    //��Ҳ������ƶ�ʱ���ø÷���
    public void Defeated()
    {
        canMove = false;
    }
}
