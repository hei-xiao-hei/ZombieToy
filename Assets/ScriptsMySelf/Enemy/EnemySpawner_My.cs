using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner_My : MonoBehaviour
{
    [Header("Spawner Properties")]
    [SerializeField] GameObject enemyPrefab;//�洢���ɵĵ��˵�Ԥ����
    [SerializeField] float spawnRate = 5f;//���ɵļ��ʱ��
    [SerializeField] int maxEnemies = 10;//�������������ֵ

    [Header("Debugging Properties")]
    [SerializeField] public bool canSpawn = false;//��������

    EnemyHealth_My[] enemies;//���е���
    WaitForSeconds spawnDelay;

    private void Awake()
    {

       // Instance = this;//����ģʽ��ֵ

        enemies = new EnemyHealth_My[maxEnemies];

        //����ʼ������maxEnemies�����˷��������У��������Ȳ���ʾ
        for (int i=0;i<maxEnemies;i++)
        {
            GameObject obj = (GameObject)Instantiate(enemyPrefab);//���ɵ���

            EnemyHealth_My enemy = obj.GetComponent<EnemyHealth_My>();

            obj.transform.parent = transform;//�������ɵĵ��˵ĸ�����

            obj.SetActive(false);

            enemies[i]=enemy;
        }
        spawnDelay = new WaitForSeconds(spawnRate);
    }

    //��ѡ���ɫ֮�������ɵ��˵�Э��
    public void StarIEnumerator()
    {
        StartCoroutine("GenerateEnemy");
    }

    //��Update֮ǰʹ��Э��Start,ÿ�����ɵ�
    IEnumerator GenerateEnemy()
    {
        //��������ɵ���
        while(canSpawn)
        {
            yield return spawnDelay;//��������
            SpawnEnemy();//���ɵ���
        }
    }

    //��ʾ����
    void SpawnEnemy()
    {
        for(int i=0;i<enemies.Length;i++)
        {
            //��������Ative����Ϊfalse�����������ˣ�
            if(!enemies[i].gameObject.activeSelf)
            {
                //���õ��˵�λ�ú���תδ��ʼ��λ�ú���ת
                enemies[i].transform.position = transform.position;
                enemies[i].transform.rotation = transform.rotation;

                //��ʾ����
                enemies[i].gameObject.SetActive(true);

                //�˳��÷�������return���Ա�֤һ��ͬʱ��ʾ�������
                return;
            }
        }
    }
}
