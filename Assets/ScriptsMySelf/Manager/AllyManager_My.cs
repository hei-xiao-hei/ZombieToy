using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllyManager_My : MonoBehaviour
{
    [SerializeField] int allyCost;//�ٻ��������Ҫ�ĵ���
    [SerializeField] GameObject allyPrefab;//���Ԥ����
    [SerializeField] Transform allySpawnPoint;//�ٻ������λ��
    [SerializeField] Image allyImage;//���Image

    //��ȡ����ű��еĲ���
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
