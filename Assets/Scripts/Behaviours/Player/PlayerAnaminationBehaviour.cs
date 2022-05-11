using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnaminationBehaviour : MonoBehaviour
{
    [Header("Component References")]
    public Animator playerAnimator;

    //String Ids
    private int playerMovementAnimationID;
    private int playerAttackAnimationID;

    public void SetupBehavior()
    {
        SetupAnimationIDs();
    }

    void SetupAnimationIDs()
    {

    }

    public void UpdateMovementAnimation(float movementBlendValue)
    {
        //playerAnimator.SetFloat(playerMovementAnimationID, movementBlendValue);
    }
    public void PlayAttackAnimation(string triggerName)
    {
        playerAnimator.SetTrigger(triggerName);
    }    

    public void PlayAttackAnimation()
    {
        playerAnimator.SetTrigger("LightPunch");
    }
}

