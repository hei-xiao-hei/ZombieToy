using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner_My : MonoBehaviour
{
    [Header("Spawner Properties")]
    [SerializeField] GameObject enemyPrefab;//存储生成的敌人的预制体
    [SerializeField] float spawnRate = 5f;//生成的间隔时间
    [SerializeField] int maxEnemies = 10;//敌人数量的最大值

    [Header("Debugging Properties")]
    [SerializeField] public bool canSpawn = false;//可以生成

    EnemyHealth_My[] enemies;//所有敌人
    WaitForSeconds spawnDelay;

    private void Awake()
    {

       // Instance = this;//单例模式赋值

        enemies = new EnemyHealth_My[maxEnemies];

        //将初始化生成maxEnemies个敌人放入数组中，但敌人先不显示
        for (int i=0;i<maxEnemies;i++)
        {
            GameObject obj = (GameObject)Instantiate(enemyPrefab);//生成敌人

            EnemyHealth_My enemy = obj.GetComponent<EnemyHealth_My>();

            obj.transform.parent = transform;//设置生成的敌人的父物体

            obj.SetActive(false);

            enemies[i]=enemy;
        }
        spawnDelay = new WaitForSeconds(spawnRate);
    }

    //当选择角色之后开启生成敌人的协程
    public void StarIEnumerator()
    {
        StartCoroutine("GenerateEnemy");
    }

    //在Update之前使用协程Start,每次生成的
    IEnumerator GenerateEnemy()
    {
        //如果能生成敌人
        while(canSpawn)
        {
            yield return spawnDelay;//间隔几秒后
            SpawnEnemy();//生成敌人
        }
    }

    //显示敌人
    void SpawnEnemy()
    {
        for(int i=0;i<enemies.Length;i++)
        {
            //如果自身的Ative属性为false（敌人隐藏了）
            if(!enemies[i].gameObject.activeSelf)
            {
                //设置敌人的位置和旋转未初始的位置和旋转
                enemies[i].transform.position = transform.position;
                enemies[i].transform.rotation = transform.rotation;

                //显示敌人
                enemies[i].gameObject.SetActive(true);

                //退出该方法，此return可以保证一次同时显示多个敌人
                return;
            }
        }
    }
}
