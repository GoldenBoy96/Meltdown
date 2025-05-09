using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHelper : MonoBehaviour
{
    public static void PlayAnimation(Animator animator, string animState)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName(animState))
        {
            animator.Play(animState);
        }
    }
}
