using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllyManager_My : MonoBehaviour
{
    [SerializeField] int allyCost;//召唤羊儿所需要的点数
    [SerializeField] GameObject allyPrefab;//羊儿预制体
    [SerializeField] Transform allySpawnPoint;//召唤羊儿的位置
    [SerializeField] Image allyImage;//羊儿Image

    //获取羊儿脚本中的参数
    Ally_My ally;
    int allyPoints;

    private void Awake()
    {
        GameObject obj = (GameObject)Instantiate(allyPrefab);

        obj.transform.parent = transform;

        ally = obj.GetComponent<Ally_My>();

        obj.SetActive(false);

        if(allyImage!=null)
        {
            allyImage.enabled = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
