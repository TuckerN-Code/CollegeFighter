using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

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

        public void onLight(InputAction.CallbackContext value)
        {
            if (value.started)
            {
                playerAnaminationBehaviour.PlayAttackAnimation();
            }
        }

        public void onHeavy(InputAction.CallbackContext value)
        {

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
            playerMovementBehavour.UpdateMovementData(smoothInputMovement);
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
