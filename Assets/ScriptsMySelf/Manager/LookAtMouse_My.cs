using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse_My : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //鼠标单例赋值了并且鼠标处于可使用状态
        if(MouseLocation_My.Instance!=null&&MouseLocation_My.Instance.IsValid)
        {
            transform.LookAt(MouseLocation_My.Instance.MousePosition);
        }
    }
}
