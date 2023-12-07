using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineAnimator : MonoBehaviour
{
    private Animator animator;

    private const string CLOSEUP = "CloseUp";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void CameraCloseUp()
    {
        animator.SetTrigger(CLOSEUP);
    }

    public void SetTimeScaleNormalThroughAnimator() 
    {
        TimeManager.Instance.SetTimeScaleNormal = true;
    }
}
