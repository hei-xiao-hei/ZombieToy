using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorDisabler_My : MonoBehaviour
{
    [SerializeField] Animator animator;//��ȡ����״̬��

    private void Reset()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        animator.enabled = false;
    }
}
