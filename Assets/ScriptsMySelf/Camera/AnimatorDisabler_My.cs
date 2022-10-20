using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorDisabler_My : MonoBehaviour
{
    [SerializeField] Animator animator;//获取动画状态机

    private void Reset()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        animator.enabled = false;
    }
}
