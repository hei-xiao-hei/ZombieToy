using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth_My : MonoBehaviour
{
    [HideInInspector]public EnemySpawner_My Spawner;

    [Header("Health Proerties")]
    [SerializeField] int maxHealth = 100;//���˵�����ֵ
    [SerializeField] int scoreValue = 10;//���˵ķ���ֵ

    [Header("Defeated Effects")]
    [SerializeField] float sinkSpeed = 0.5f;//�����ƶ��ٶ� 
    [SerializeField] float deathEffectTime = 2f;//����������ʱСʱ��ʱ��
    [SerializeField] AudioClip deathClip = null;//������Ч
    [SerializeField] AudioClip hurtClip = null;//������Ч

    [Header("Script References")]
    [SerializeField] EnemyAttack_My enemyAttack;
    [SerializeField] EnemyMovement_My enemyMovement;

    [Header("Components")]
    [SerializeField] Animator animator;
    [SerializeField] AudioSource audioSource;
    [SerializeField] CapsuleCollider capsuleCollider;
    [SerializeField] ParticleSystem hitParticles;//ParticleSystem��Unityר������������Ч��ϵͳ

    [Header("Debugging Properties")]
    [SerializeField] bool isInvulnerable;//�����Ƿ�������״̬

    int currentHealth;//���˵�ǰѪ��
    bool isSinking;//�����Ƿ�����

    private void Reset()
    {
        enemyAttack = GetComponent<EnemyAttack_My>();
        enemyMovement = GetComponent<EnemyMovement_My>();

        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        hitParticles = GetComponent<ParticleSystem>();
    }

    //���ű��������ʱ��
    private void OnEnable()
    {
        currentHealth = maxHealth;//���˵�ǰ��Ѫ������ʼֵ
        isSinking = false;
        capsuleCollider.isTrigger = false;//����Ϊ��ײ��

        if(audioSource!=null)
        {
            audioSource.clip = hurtClip;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(!isSinking)
        {
            return;
        }
        //����λ��
        transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
    }

    //��������ʱ���ô˽ű�
    public void TakeDamage(int amount)
    {
        //������˵�ǰѪ��С�ڵ���0�����ߴ�������״ֱ̬��return
        if(currentHealth<=0||isInvulnerable)
        {
            return;
        }
        //���˼�Ѫ��
        currentHealth -= amount;

        //�����ǰѪ��С�ڵ���0������������ű�
        if(currentHealth<=0)
        {
            Defeated();
        }
        //���ŵ���������Ч
        if(audioSource!=null)
        {
            audioSource.Play();
        }
        //���ŵ����������ӡ�
        hitParticles.Play();
    }

    //������������
    void Defeated()
    {
        capsuleCollider.isTrigger = true;//�����������д�����
        enemyMovement.Defeated();//��������ƶ��������������رյ�����
        animator.enabled = true;//��������״̬��
        animator.SetTrigger("Dead");//������������

        if(audioSource!=null)
        {
            audioSource.clip = deathClip;
        }

        //TO DO���˹������ƶ�������ķ���

        GameManager_My.Instance.AddScore(scoreValue);//�����ҷ���

        Invoke("TurnOff", deathEffectTime);//��ʱ����󣬵���TurnOff����
    }

    //�����ʧ����
    void TurnOff()
    {
        gameObject.SetActive(false);
    }

    //û����ⷽ����ɶ��
    public void StartSinking()
    {
        isSinking = true;
    }
}
