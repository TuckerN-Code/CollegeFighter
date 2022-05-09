using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerBehavior
{
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
            //playerMovementAnimationID = Animator.StringToHash("Movement");
            playerAttackAnimationID = Animator.StringToHash("LightPunch");
        }

        public void UpdateMovementAnimation(float movementBlendValue)
        {
            //playerAnimator.SetFloat(playerMovementAnimationID, movementBlendValue);
        }

        public void PlayAttackAnimation()
        {
            playerAnimator.SetTrigger("LightPunch");
        }
    }
}
