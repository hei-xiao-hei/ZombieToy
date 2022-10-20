using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow_My : MonoBehaviour
{
    [SerializeField] float smoothing = 5.0f;
    [SerializeField] Vector3 offset = new Vector3(0, 5, -8);


    // Update is called once per frame
    void FixedUpdate()
    {
        //获取玩家的位置，然后计算出相机和玩家之间的间距。
        Vector3 targetCampos = GameManager_My.Instance.Player.transform.position + offset;
        //更改相机位置
        transform.position = Vector3.Lerp(transform.position, targetCampos, smoothing * Time.deltaTime);
    }
}
