using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using 

namespace PlayerBehavior
{
    public class PlayerController : MonoBehaviour
    {
        //Player ID
        private int playerID;

        [Header("Sub Behaviors")]
        public PlayerMovementBehavour playerMovementBehavour;
        public PlayerAnaminationBehaviour playerAnaminationBehaviour;
        public PlayerVisualsBehaviour playerVisualsBehaviour;

        [Header("Input Settings")]
        public PlayerInput playerInput;
        public float movementSmoothingSpeed = 1f;
        private Vector3 rawInputMovement;
        private Vector3 smoothInputMovement;

        //Action Maps
        private string actionMapPlayerControls = "PlayerOne";
        private string actionMapMenuControls = "Menu Controls";

        private string currentControlScheme;


        //Player state
        private PlayerStateManager playerState;

        // Start is called before the first frame update
        public void SetupPlayer(int newPlayerID)
        { 
            playerID = newPlayerID;
            currentControlScheme = playerInput.currentControlScheme;

            playerMovementBehavour.SetupBehaviour();
            playerAnaminationBehaviour.SetupBehavior();
            playerVisualsBehaviour.SetupBehaviour();
        }


        //Input System Action Methods
        //This is from playerInput when the actions are done.
        //It stores the input vector

        public void OnMovement(InputAction.CallbackContext value)
        {
            Vector2 inputMovement = value.ReadValue<Vector2>();
            rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
        }

        public void onJump(InputAction.CallbackContext value)
        {
            if (value.started)
            { 
                //add input to storage

                if(playerState.state_Able_To_Grounded_Move)
                {
                    //Grounded Jump code
                }
                else if(playerState.state_Able_To_Airborne_Move)
                {
                    //Airborne jump code
                }
                else if(playerState.state_Able_To_Jump_Cancel)
                {
                    //Jump cancel code
                }
                else
                {
                    //do not jump
                }
            }
        }

        public void onLight(InputAction.CallbackContext value)
        {
            if (value.started)
            {
                //Add input to storage

                switch (playerState.state_Position)
                {
                    case positional_State.Airborn:
                        //Jump light attack handling
                        break;
                    case positional_State.Standing:
                        //Standing light attack handling
                        break;
                    case positional_State.Crouching:
                        //Crouching light attack handling
                        break;
                }
                playerAnaminationBehaviour.PlayAttackAnimation();
            }
            if (value.canceled)
            {
                //Add release input to storage
            }
        }

        public void onHeavy(InputAction.CallbackContext value)
        {
            if (value.started)
            {
                //Add input to storage
                switch (playerState.state_Position)
                {
                    case positional_State.Airborn:
                        //Jump heavy attack handling
                        break;
                    case positional_State.Standing:
                        //Standing heavy attack handling
                        break;
                    case positional_State.Crouching:
                        //Crouching heavy attack handling
                        break;
                }
            }
            if (value.canceled)
            {
                //Add release input to storage
            }
        }

        public void onForward(InputAction.CallbackContext value)
        {
            if (value.started)
            {
                //Add input to storage
                switch(playerState.state_Movement)
                {
                    case (Movement_State.Able_To_Grounded_Move):
                        Vector2 inputMovement = new Vector2(.01f, 0);
                        rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
                        break;
                    case (Movement_State.Able_To_Airbone_Move):
                        //Jump movement code
                        break;
                }
            }
            else if (value.canceled)
            {
                switch(playerState.state_Movement)
                {
                    case (Movement_State.Able_To_Grounded_Move):
                        Vector2 inputMovement = new Vector2(0, 0);
                        rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
                        break;
                    case (Movement_State.Able_To_Airbone_Move):
                        //Airborne movement code
                        break;
                }
       
            }
        }

        public void onBack(InputAction.CallbackContext value)
        {
            if (value.started)
            {
                //Add input to storage
                switch (playerState.state_Movement)
                {
                    case (Movement_State.Able_To_Grounded_Move):
                        Vector2 inputMovement = new Vector2(-.01f, 0);
                        rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
                        break;
                    case (Movement_State.Able_To_Airbone_Move):
                        //Jump movement code
                        break;
                }
            }
            else if (value.canceled)
            {
                switch (playerState.state_Movement)
                {
                    case (Movement_State.Able_To_Grounded_Move):
                        Vector2 inputMovement = new Vector2(0, 0);
                        rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
                        break;
                    case (Movement_State.Able_To_Airbone_Move):
                        //Airborne movement code
                        break;
                }

            }
        }

        public void OnTogglePause(InputAction.CallbackContext value)
        {
            if(value.started)
            {
                //GameManager.Instance.TogglePauseState(this);
            }
        }

        //Input System Callbacks (when device is changed)

        public void OnControlsChanged()
        {
            if(playerInput.currentControlScheme != currentControlScheme)
            {
                currentControlScheme = playerInput.currentControlScheme;

                playerVisualsBehaviour.UpdatePlayerVisuals();
                RemoveAllBindingOverrides();
            }
        }

        //Input System callbacks for disconnect
        void OnDeviceLost()
        {
            playerVisualsBehaviour.SetDisconnectedDeviceVisuals();
        }

        void OnDeviceRegained()
        {
            StartCoroutine(WaitForDeviceToRegained());
        }

        IEnumerator WaitForDeviceToRegained()
        {
            yield return new WaitForSeconds(0.1f);
            playerVisualsBehaviour.UpdatePlayerVisuals();
        }

        // Update is called once per frame
        void Update()
        {
            CalculateMovementInputSmoothing();
            UpdatePlayerMovement();
            UpdatePlayerAnimationMovement();
        }

        void CalculateMovementInputSmoothing()
        {
            smoothInputMovement = Vector3.Lerp(smoothInputMovement,
                rawInputMovement,
                Time.deltaTime * movementSmoothingSpeed);
        }

        void UpdatePlayerMovement()
        {
            playerMovementBehavour.UpdateMovementData(rawInputMovement);
        }

        void UpdatePlayerAnimationMovement()
        {
            playerAnaminationBehaviour.UpdateMovementAnimation(smoothInputMovement.magnitude);
        }

        public void SetInputActiveState(bool gameIsPaused)
        {
            switch (gameIsPaused)
            {
                case true:
                    playerInput.DeactivateInput();
                    break;
                case false:
                    playerInput.ActivateInput();
                    break;
            }

        }

        void RemoveAllBindingOverrides()
        {
            InputActionRebindingExtensions.RemoveAllBindingOverrides(playerInput.currentActionMap);
        }

        public void EnableGameplayControls()
        {
            playerInput.SwitchCurrentActionMap(actionMapPlayerControls);
        }

        public void EnablePauseMenuControls()
        {
            playerInput.SwitchCurrentActionMap(actionMapMenuControls);
        }
    }
}
