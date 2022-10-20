using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpotlight_My : MonoBehaviour
{
    [SerializeField] GameObject spotlightPC;
    [SerializeField] GameObject spotlightMoblie;
    // Start is called before the first frame update
    void Awake()
    {
        //根据不同的设备调用不同的灯
#if UNITY_ANDROID || UNITY_IOS || UNITY_WP8
        spotlightMoblie.SetActive(true);
#else
        spotlightPC.SetActive(true);
#endif
    }

    // Update is called once per frame
    void Update()
    {
        //游戏开始后，关闭灯光。
        if(GameManager_My.Instance.Player!=null)
        {
            gameObject.SetActive(false);
        }
    }
}
