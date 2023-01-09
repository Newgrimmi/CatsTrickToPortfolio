using System;
using UnityEngine;

public class Curtain : MonoBehaviour
{
    private Action savedCallBackCurtain;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void InitializeAfterTarget(Action callback)
    {
        anim.SetBool("IsRun", true);
        savedCallBackCurtain = callback;
    }
    public void InitializeAfterLose(Action callback)
    {
        anim.SetBool("IsRunBack", true);
        savedCallBackCurtain = callback;
    }

    public void RunEnd()
    {
        anim.SetBool("IsRun", false);
        anim.SetBool("IsRunBack", false);
        gameObject.SetActive(false);
        savedCallBackCurtain?.Invoke();
    }
}
