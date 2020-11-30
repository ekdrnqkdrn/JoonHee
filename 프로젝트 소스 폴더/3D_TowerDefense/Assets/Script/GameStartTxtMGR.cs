using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartTxtMGR : MonoBehaviour
{
    public Animator m_Animator;

    void Update()
    {
        if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            gameObject.SetActive(false);
        }
    }
}
